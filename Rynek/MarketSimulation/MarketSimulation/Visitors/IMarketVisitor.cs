using MarketSimulation.Data;

namespace MarketSimulation.Visitors
{
    public interface IMarketVisitor
    {
        decimal Visit(Seller seller);
        decimal Visit(Buyer buyer);
    }
}
