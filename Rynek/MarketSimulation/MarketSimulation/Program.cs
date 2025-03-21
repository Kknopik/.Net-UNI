using System;
using MarketSimulation.Data;
using MarketSimulation.Observers;
using MarketSimulation.Visitors;
using MarketSimulation.Logging;

namespace MarketSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstNeedsProduct = new FirstNeedsProduct("Bread", 2.50m, 10);
            var luxuryProduct = new LuxuryProduct("Laptop", 1000.00m, 5);

            var seller = new Seller("TechSeller", luxuryProduct, 500, 0.05m, 0.2m, 0.3m);
            var buyer = new Buyer("John Doe", 1200.00m, 0.05m, true);
            var bank = new Bank();

            var products = new Product[] { firstNeedsProduct, luxuryProduct };

            var market = new Market(seller, buyer, bank, products);
            market.SimulateMarket();

            Console.ReadLine();
        }
    }
}
