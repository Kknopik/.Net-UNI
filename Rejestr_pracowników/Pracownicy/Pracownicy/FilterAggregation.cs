public class FilterAggregation : IRegister
{
    private readonly List<IRegister> filters = new List<IRegister>();

    public void AddFilter(IRegister filter)
    {
        filters.Add(filter);
    }

    public IEnumerable<Employee> Execute(IEnumerable<Employee> employees)
    {
        IEnumerable<Employee> result = employees;

        foreach (var filter in filters)
        {
            result = filter.Execute(result);
        }

        return result;
    }
}
