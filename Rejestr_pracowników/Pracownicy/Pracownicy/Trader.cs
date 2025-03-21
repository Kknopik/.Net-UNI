public enum Effectiveness 
{
    Low = 60,
    Medium = 90,
    High = 120
}

public class Trader : Employee
{
    public Effectiveness Effectiveness { get; set; }
    public double CommissionAmount { get; set; }

    public override double CountValue()
    {
        return Experience * (double)Effectiveness;
    }
}