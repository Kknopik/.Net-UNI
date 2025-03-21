using MarketSimulation.Data;
using MarketSimulation.Logging;

namespace MarketSimulation.Observers
{
    public class BankObserver : IObserver
    {
        public void Update(object subject)
        {
            if (subject is Bank bank)
            {
                LogService.Log($"BankObserver: Current bank inflation rate is {bank.InflationRate * 100:F2}%.");
            }
        }
    }
}
