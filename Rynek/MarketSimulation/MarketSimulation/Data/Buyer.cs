using System;
using MarketSimulation.Visitors;
using MarketSimulation.Logging;

namespace MarketSimulation.Data
{
    public class Buyer
    {
        public string Name { get; set; }
        public decimal Money { get; set; }
        public decimal InflationExpectation { get; set; }
        public bool WantsLuxuryItem { get; set; }

        public Buyer(string name, decimal money, decimal inflationExpectation, bool wantsLuxuryItem)
        {
            Name = name;
            Money = money;
            InflationExpectation = inflationExpectation;
            WantsLuxuryItem = wantsLuxuryItem;
        }

        public bool CanAfford(Product product)
        {
            return Money >= product.Price;
        }

        public void BuyProduct(Seller seller)
        {
            if (CanAfford(seller.Product))
            {
                Money -= seller.Product.Price;
                LogService.Log($"{Name} bought {seller.Product.Name} for {seller.Product.Price:C}.");
            }
            else
            {
                LogService.Log($"{Name} cannot afford {seller.Product.Name}.");
            }
        }

        public void BuyProducts(Product[] products)
        {
            foreach (var product in products)
            {
                if (product is FirstNeedsProduct && product.CanBuyProduct(this))
                {
                    product.BuyProduct(this);
                }
            }

            if (WantsLuxuryItem)
            {
                foreach (var product in products)
                {
                    if (product is LuxuryProduct && product.CanBuyProduct(this))
                    {
                        product.BuyProduct(this);
                    }
                }
            }
        }

        public decimal Accept(IMarketVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
