public abstract class Employee 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set;}
    public int Age {get; set; }
    public int Experience { get; set; }
    public Address Address { get; set; }

    public abstract double CountValue(); 
}