public class CityFilter : IRegister
{
    private readonly string city;

    public CityFilter(string city)
    {
        this.city = city;
    }

    public IEnumerable<Employee> Execute(IEnumerable<Employee> employees)
    {
        return employees.Where(e => e.Address.City.Equals(city, StringComparison.OrdinalIgnoreCase));
    }
}
