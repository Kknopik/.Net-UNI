using MarketSimulation.Data;

namespace MarketSimulation.Visitors
{
    public class ProfitVisitor : IMarketVisitor
    {
        public decimal Visit(Seller seller)
        {
            return seller.Product.Price - seller.ManufacturingCost;
        }

        public decimal Visit(Buyer buyer)
        {
            return 0;
        }
    }
}
