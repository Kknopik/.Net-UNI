public class AgeFilter : IRegister
{
    public IEnumerable<Employee> Execute(IEnumerable<Employee> employees)
    {
        return employees.OrderBy(e => e.Age);
    }
}
