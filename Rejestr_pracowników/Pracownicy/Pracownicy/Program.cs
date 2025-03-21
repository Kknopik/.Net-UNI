var register = new Register();

register.AddEmployee(new OfficeWorker
{
    Id = 1,
    Name = "Anna",
    LastName = "Kowalska",
    Age = 30,
    Experience = 5,
    Intellect = 120,
    Address = new Address { City = "Warszawa", Street = "Piekna", BuildingNr = 10, FlatNr = 5 }
});

register.AddEmployee(new OfficeWorker
{
    Id = 2,
    Name = "Jan",
    LastName = "Nowak",
    Age = 45,
    Experience = 15,
    Intellect = 135,
    Address = new Address { City = "Kraków", Street = "Zielona", BuildingNr = 20, FlatNr = 7 }
});

register.AddEmployee(new PhysicalWorker
{
    Id = 3,
    Name = "Piotr",
    LastName = "Wójcik",
    Age = 40,
    Experience = 12,
    Strength = 85,
    Address = new Address { City = "Warszawa", Street = "Słoneczna", BuildingNr = 15, FlatNr = 12 }
});

register.AddEmployee(new PhysicalWorker
{
    Id = 4,
    Name = "Tomasz",
    LastName = "Kaczmarek",
    Age = 25,
    Experience = 3,
    Strength = 90,
    Address = new Address { City = "Kraków", Street = "Wiosenna", BuildingNr = 5, FlatNr = 2 }
});

register.AddEmployee(new Trader
{
    Id = 5,
    Name = "Katarzyna",
    LastName = "Lewandowska",
    Age = 35,
    Experience = 8,
    Effectiveness = Effectiveness.High,
    CommissionAmount = 10,
    Address = new Address { City = "Warszawa", Street = "Długa", BuildingNr = 25, FlatNr = 8 }
});

var filterAggregation = new FilterAggregation();
filterAggregation.AddFilter(new AgeFilter());
filterAggregation.AddFilter(new CityFilter("Warszawa"));

var result = register.ExecuteFilter(filterAggregation);

foreach (var employee in result)
{
    Console.WriteLine($"{employee.Name} {employee.LastName} - {employee.Age} - {employee.Experience} - {employee.Address}");
}
