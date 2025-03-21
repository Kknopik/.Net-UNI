using System.Dynamic;

public class Address
{
    public string Street { get; set; }
    public int BuildingNr { get; set; }
    public int FlatNr { get; set; }
    public string City { get; set; }

     public override string ToString()
    {
        return $"{Street} {BuildingNr}/{FlatNr}, {City}";
    }
}