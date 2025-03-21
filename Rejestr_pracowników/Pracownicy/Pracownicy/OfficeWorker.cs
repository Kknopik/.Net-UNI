public class OfficeWorker : Employee
{
    public int OfficeId { get; set; }
    public int Intellect { get; set; }

    public override double CountValue()
    {
        return Experience * Intellect;
    }
}