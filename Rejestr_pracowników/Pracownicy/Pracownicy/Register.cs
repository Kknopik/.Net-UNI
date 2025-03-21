public class Register
{
    private readonly List<Employee> employees = new List<Employee>();

    public void AddEmployee(Employee employee)
    {
        if (employees.Any(e => e.Id == employee.Id))
        {
            throw new InvalidOperationException("Employee with this ID already exists.");
        }
        employees.Add(employee);
    }

    public void AddEmployees(IEnumerable<Employee> newEmployees)
    {
        foreach (var employee in newEmployees)
        {
            if (employees.Any(e => e.Id == employee.Id))
            {
                throw new InvalidOperationException("Employee with this ID already exists.");
            }
            employees.Add(employee);
        }
    }


    public void DeleteEmployee(int id)
    {
        employees.RemoveAll(e => e.Id == id);
    }

    public IEnumerable<Employee> ExecuteFilter(IRegister filter)
    {
        return filter.Execute(employees);
    }
}
