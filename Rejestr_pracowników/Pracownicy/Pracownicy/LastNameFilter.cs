public class LastNameFilter : IRegister
{
    public IEnumerable<Employee> Execute(IEnumerable<Employee> employees)
    {
        return employees.OrderBy(e => e.LastName);
    }
}
