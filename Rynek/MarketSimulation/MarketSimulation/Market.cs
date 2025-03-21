using MarketSimulation.Data;
using MarketSimulation.Observers;
using MarketSimulation.Visitors;
using MarketSimulation.Logging;

namespace MarketSimulation
{
    public class Market
    {
        private Seller _seller;
        private Buyer _buyer;
        private Bank _bank;
        private Product[] _products;

        public Market(Seller seller, Buyer buyer, Bank bank, Product[] products)
        {
            _seller = seller;
            _buyer = buyer;
            _bank = bank;
            _products = products;
        }

        public void SimulateMarket()
        {
            LogService.Log($"Market started with {string.Join(", ", _products.Select(p => p.ToString()))}.");

            var priceObserver = new PriceObserver(_seller);
            var inflationObserver = new InflationObserver(_bank);
            var bankObserver = new BankObserver();

            _bank.SetInflation(0.07m);
            LogService.Log($"Inflation updated to {_bank.InflationRate * 100}%.");

            _seller.Accept(new PriceVisitor());
            _buyer.Accept(new PriceVisitor());

            _buyer.BuyProducts(_products);

            _seller.SellProduct(_buyer);

            var profitVisitor = new ProfitVisitor();
            LogService.Log($"Seller's profit: {_seller.Accept(profitVisitor):C}");
        }
    }
}
