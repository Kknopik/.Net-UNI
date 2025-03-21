using MarketSimulation.Logging;

namespace MarketSimulation.Data
{
    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        protected Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public bool CanBuyProduct(Buyer buyer)
        {
            return buyer.Money >= Price && Quantity > 0;
        }

        public void BuyProduct(Buyer buyer)
        {
            if (CanBuyProduct(buyer))
            {
                buyer.Money -= Price;
                Quantity--;
                LogService.Log($"{buyer.Name} bought {Name} for {Price:C}. Remaining quantity: {Quantity}");
            }
            else
            {
                LogService.Log($"{buyer.Name} cannot afford {Name} or it's out of stock.");
            }
        }
    }

    public class FirstNeedsProduct : Product
    {
        public FirstNeedsProduct(string name, decimal price, int quantity)
            : base(name, price, quantity) { }
    }

    public class LuxuryProduct : Product
    {
        public LuxuryProduct(string name, decimal price, int quantity)
            : base(name, price, quantity) { }
    }
}
