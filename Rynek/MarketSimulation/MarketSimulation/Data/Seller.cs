using System;
using MarketSimulation.Visitors;
using MarketSimulation.Logging;

namespace MarketSimulation.Data
{
    public class Seller
    {
        public string Name { get; set; }
        public Product Product { get; set; }
        public decimal ManufacturingCost { get; set; }
        public decimal InflationRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Margin { get; set; }

        public Seller(string name, Product product, decimal manufacturingCost, decimal inflationRate, decimal taxRate, decimal margin)
        {
            Name = name;
            Product = product;
            ManufacturingCost = manufacturingCost;
            InflationRate = inflationRate;
            TaxRate = taxRate;
            Margin = margin;
        }

        public decimal GetPrice()
        {
            return (ManufacturingCost + (ManufacturingCost * InflationRate) + (ManufacturingCost * TaxRate)) * (1 + Margin);
        }

        public void SellProduct(Buyer buyer)
        {
            if (buyer.CanAfford(Product))
            {
                buyer.Money -= Product.Price;
                LogService.Log($"{Name} sold {Product.Name} to {buyer.Name} for {Product.Price:C}.");

                Product.Price = GetPrice();
                LogService.Log($"{Name} updated price of {Product.Name} to {Product.Price:C} after inflation.");
            }
            else
            {
                LogService.Log($"{buyer.Name} cannot afford {Product.Name}.");
            }
        }

        public decimal Accept(IMarketVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
