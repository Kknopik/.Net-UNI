public class ExperienceFilter : IRegister
{
    public IEnumerable<Employee> Execute(IEnumerable<Employee> employees)
    {
        return employees.OrderByDescending(e => e.Experience);
    }
}
