using MarketSimulation.Data;
using MarketSimulation.Logging;

namespace MarketSimulation.Observers
{
    public class PriceObserver : IObserver
    {
        private Seller _seller;

        public PriceObserver(Seller seller)
        {
            _seller = seller;
        }

        public void Update(object subject)
        {
            if (subject is Seller seller)
                {
                    var oldPrice = seller.Product.Price;
                    seller.Product.Price = seller.GetPrice();
                    LogService.Log($"PriceObserver: Updated product price from {oldPrice:C} to {seller.Product.Price:C}.");
                }    
        }
    }
}
