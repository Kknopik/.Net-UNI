using NUnit.Framework;
using System;
using MarketSimulation.Data;
using MarketSimulation.Observers;
using MarketSimulation.Logging;
using MarketSimulation.Visitors;

namespace MarketSimulation.Tests
{
    [TestFixture]
    public class MarketSimulationTests
    {
        private Bank _bank;
        private Seller _seller;
        private Buyer _buyer;
        private Product _product;
        private Product _luxuryProduct;

        [SetUp]
        public void SetUp()
        {
            _product = new FirstNeedsProduct("Bread", 2.50m, 10);
            _luxuryProduct = new LuxuryProduct("Laptop", 1000.00m, 5);

            _seller = new Seller("TechSeller", _luxuryProduct, 500, 0.05m, 0.2m, 0.3m);
            _buyer = new Buyer("John Doe", 1200.00m, 0.05m, true);
            _bank = new Bank();
        }

        [Test]
        public void Bank_SetInflation_IncreasesInflationWhenBelowThreshold()
        {
            var initialInflation = _bank.InflationRate;

            _bank.SetInflation(0.03m);

            Assert.That(_bank.InflationRate, Is.EqualTo(initialInflation + 0.05m));
        }

        [Test]
        public void Bank_SetInflation_DecreasesInflationWhenAboveThreshold()
        {
            _bank.SetInflation(0.60m);

            Assert.That(_bank.InflationRate, Is.EqualTo(0.05m));
        }

        [Test]
        public void Bank_SetInflation_SetsCorrectInflationWithinRange()
        {
            _bank.SetInflation(0.15m);

            Assert.That(_bank.InflationRate, Is.EqualTo(0.15m));
        }

        [Test]
        public void Seller_GetPrice_CalculatesPriceCorrectly()
        {
            var price = _seller.GetPrice();

            Assert.That(price, Is.EqualTo(812.5));
        }

        [Test]
        public void Seller_SellProduct_DecreasesBuyerMoneyWhenProductIsPurchased()
        {
            _seller.SellProduct(_buyer);

            Assert.That(_buyer.Money, Is.EqualTo(200m));
        }

        [Test]
        public void Seller_SellProduct_LogsTransaction()
        {
            LogService.ClearLog();

            var initialPrice = _seller.Product.Price;

            _seller.SellProduct(_buyer);

            var logMessages = LogService.GetLogMessages();
            
            Assert.That(logMessages, Is.EqualTo(new[] {
                $"TechSeller sold {_seller.Product.Name} to {_buyer.Name} for {initialPrice:C}.",
                $"TechSeller updated price of {_seller.Product.Name} to {_seller.Product.Price:C} after inflation."
            }));
        }

        [Test]
        public void Buyer_CanAfford_ReturnsTrue_WhenBuyerHasEnoughMoney()
        {
            var canAfford = _buyer.CanAfford(_luxuryProduct);

            Assert.That(canAfford, Is.True);
        }

        [Test]
        public void Buyer_CanAfford_ReturnsFalse_WhenBuyerDoesNotHaveEnoughMoney()
        {
            _buyer.Money = 500m;

            var canAfford = _buyer.CanAfford(_luxuryProduct);

            Assert.That(canAfford, Is.False);
        }

        [Test]
        public void PriceObserver_UpdatesProductPrice_WhenSellerPriceChanges()
        {
            LogService.ClearLog();
            var priceObserver = new PriceObserver(_seller);
            _seller.Product.Price = _seller.GetPrice();

            priceObserver.Update(_seller);

            var logMessages = LogService.GetLogMessages();
            Assert.That(logMessages, Does.Contain("PriceObserver: Updated product price from 812,50 zł to 812,50 zł."));
        }

        [Test]
        public void InflationObserver_LogsInflationRateUpdate_WhenInflationIsChanged()
        {
            LogService.ClearLog();
            var inflationObserver = new InflationObserver(_bank);

            _bank.SetInflation(0.10m);

            var logMessages = LogService.GetLogMessages();
            Assert.That(logMessages, Does.Contain("InflationObserver: Inflation rate updated to 10,00%."));
        }

        [Test]
        public void Market_SimulateMarket_LogsExpectedOutput_WhenMarketIsSimulated()
        {
            var products = new Product[] { _product, _luxuryProduct };
            var market = new Market(_seller, _buyer, _bank, products);
            LogService.ClearLog();

            var initialBreadPrice = _product.Price;
            var initialLaptopPrice = _luxuryProduct.Price;

            market.SimulateMarket();

            var logMessages = LogService.GetLogMessages();
            
            Assert.That(logMessages, Is.EqualTo(new[] {
                "Market started with MarketSimulation.Data.FirstNeedsProduct, MarketSimulation.Data.LuxuryProduct.",
                "InflationObserver: Inflation rate updated to 7,00%.",
                "Inflation updated to 7,00%.",
                $"John Doe bought Bread for {initialBreadPrice:C}. Remaining quantity: 9",
                $"John Doe bought Laptop for {initialLaptopPrice:C}. Remaining quantity: 4",
                "John Doe cannot afford Laptop.",
                "Seller's profit: 500,00 zł"
            }));
        }

        [Test]
        public void Market_SimulateMarket_StabilizesAfterExtremeDisruption()
        {
            var products = new Product[] { _product, _luxuryProduct };
            var market = new Market(_seller, _buyer, _bank, products);
            LogService.ClearLog();

            var initialBreadPrice = _product.Price;
            var initialLaptopPrice = _luxuryProduct.Price;
            var initialBankInflation = _bank.InflationRate;

            _bank.SetInflation(1.50m);
            
            market.SimulateMarket();

            var disruptedBreadPrice = _product.Price;
            var disruptedLaptopPrice = _luxuryProduct.Price;
            var disruptedBankInflation = _bank.InflationRate;

            _bank.SetInflation(0.05m);
            market.SimulateMarket();

            market.SimulateMarket();

            Assert.That(_product.Price, Is.EqualTo(initialBreadPrice).Within(0.01m), "Bread price did not stabilize correctly.");
            Assert.That(_luxuryProduct.Price, Is.EqualTo(initialLaptopPrice).Within(0.01m), "Laptop price did not stabilize correctly.");
            Assert.That(_bank.InflationRate, Is.EqualTo(initialBankInflation).Within(0.02m), "Bank inflation rate did not stabilize correctly.");
        }

    }
}
