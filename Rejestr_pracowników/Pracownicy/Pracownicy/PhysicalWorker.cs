public class PhysicalWorker : Employee  
{
    public int Strength { get; set; }

    public override double CountValue()
    {
        return Experience * Strength / Age;
    }
}