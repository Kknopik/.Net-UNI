public class ValueFilter : IRegister
{
    public IEnumerable<Employee> Execute(IEnumerable<Employee> employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee.Name} {employee.LastName} - Wartość dla korporacji: {employee.CountValue()}");
        }
        return employees; // zwracamy listę bez zmian, by można było używać tej strategii w agregacjach
    }
}
