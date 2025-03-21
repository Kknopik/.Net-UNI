using MarketSimulation.Data;

namespace MarketSimulation.Visitors
{
    public class PriceVisitor : IMarketVisitor
    {
        public decimal Visit(Seller seller)
        {
            return seller.GetPrice();
        }

        public decimal Visit(Buyer buyer)
        {
            return buyer.Money;
        }
    }
}
