using System;
using System.Collections.Generic;
using MarketSimulation.Observers;

namespace MarketSimulation.Data
{
    public class Bank
    {
        public decimal InflationRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalMarketTurnover { get; set; }

        public Bank()
        {
            InflationRate = 0.05m; 
            TaxRate = 0.2m;
        }

        public void SetInflation(decimal inflationRate)
        {
            if (inflationRate <= 0.05m)
            {
                InflationRate += 0.05m;
                Console.WriteLine($"Inflation increased to {InflationRate * 100}%.");
            }
            
            else if (inflationRate >= 0.50m)
            {
                InflationRate -= 0.05m;
                if (InflationRate < 0.05m)
                {
                    InflationRate = 0.05m;
                }
                Console.WriteLine($"Inflation decreased to {InflationRate * 100}%.");
            }
            else
            {
                
                InflationRate = inflationRate;
                Console.WriteLine($"Inflation rate set to {InflationRate * 100}%."); 
            }

            NotifyObservers();
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        private readonly List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }
    }
}
