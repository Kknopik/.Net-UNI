using MarketSimulation.Data;
using MarketSimulation.Logging;

namespace MarketSimulation.Observers
{
    public class InflationObserver : IObserver
    {
        private readonly Bank _bank;

        public InflationObserver(Bank bank)
        {
            _bank = bank;
            _bank.Attach(this);
        }

        public void Update(object subject)
        {
            if (subject is Bank bank)
            {
                LogService.Log($"InflationObserver: Inflation rate updated to {bank.InflationRate * 100:F2}%.");
            }
        }
    }
}
