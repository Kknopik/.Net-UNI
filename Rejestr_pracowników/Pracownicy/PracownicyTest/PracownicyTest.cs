using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PracownicyTest
{
    [TestFixture]
    public class Pracownicytest
    {
        private Register register;

        [SetUp]
        public void Setup()
        {
            register = new Register();
            
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
        }

        [Test]
        public void TestAgeFilter()
        {
            var ageFilter = new AgeFilter();
            var result = ageFilter.Execute(register.ExecuteFilter(new ValueFilter()));

            Assert.AreEqual(3, result.First().Experience);
            Assert.AreEqual(15, result.Last().Experience);
        }

        [Test]
        public void TestCityFilter()
        {
            var cityFilter = new CityFilter("Warszawa");
            var result = cityFilter.Execute(register.ExecuteFilter(new ValueFilter()));

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void TestExperienceFilter()
        {
            var experienceFilter = new ExperienceFilter();
            var result = experienceFilter.Execute(register.ExecuteFilter(new ValueFilter()));

            Assert.AreEqual(15, result.First().Experience);
            Assert.AreEqual(3, result.Last().Experience);
        }

        [Test]
        public void TestLastNameFilter()
        {
            var lastNameFilter = new LastNameFilter();
            var result = lastNameFilter.Execute(register.ExecuteFilter(new ValueFilter()));

            Assert.AreEqual("Kaczmarek", result.First().LastName);
            Assert.AreEqual("Wójcik", result.Last().LastName);
        }

        [Test]
        public void TestFilterAggregation()
        {
            var aggregation = new FilterAggregation();
            aggregation.AddFilter(new AgeFilter());
            aggregation.AddFilter(new CityFilter("Warszawa"));

            var result = aggregation.Execute(register.ExecuteFilter(new ValueFilter()));

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Anna", result.First().Name);
        }

        [Test]
        public void TestEmployeeValueCalculation()
        {
            var officeWorker = new OfficeWorker
            {
                Experience = 5,
                Intellect = 120
            };
            var physicalWorker = new PhysicalWorker
            {
                Experience = 10,
                Age = 30,
                Strength = 80
            };
            var trader = new Trader
            {
                Experience = 8,
                Effectiveness = Effectiveness.High
            };

            Assert.AreEqual(600, officeWorker.CountValue());
            Assert.AreEqual(26.0, physicalWorker.CountValue(), 0.01);
            Assert.AreEqual(960, trader.CountValue());
        }

        [Test]
        public void TestDeleteEmployee()
        {
            register.DeleteEmployee(1);
            var result = register.ExecuteFilter(new ValueFilter());

            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void TestAddEmployees()
        {
            var newEmployees = new List<Employee>
            {
                new OfficeWorker
                {
                    Id = 6,
                    Name = "Alicja",
                    LastName = "Nowakowska",
                    Age = 28,
                    Experience = 6,
                    Intellect = 130,
                    Address = new Address { City = "Warszawa", Street = "Piekna", BuildingNr = 30, FlatNr = 6 }
                },
                new Trader
                {
                    Id = 7,
                    Name = "Michał",
                    LastName = "Lewandowski",
                    Age = 40,
                    Experience = 12,
                    Effectiveness = Effectiveness.Medium,
                    CommissionAmount = 15,
                    Address = new Address { City = "Kraków", Street = "Zielona", BuildingNr = 22, FlatNr = 4 }
                }
            };

            register.AddEmployees(newEmployees);

            var allEmployees = register.ExecuteFilter(new ValueFilter());
            Assert.AreEqual(7, allEmployees.Count());
        }

        [Test]
public void TestAddEmployee_DuplicateId_ThrowsException()
{
    var duplicateEmployee = new OfficeWorker
    {
        Id = 1,
        Name = "Adam",
        LastName = "Nowak",
        Age = 40,
        Experience = 10,
        Intellect = 130,
        Address = new Address { City = "Warszawa", Street = "Nowa", BuildingNr = 2, FlatNr = 1 }
    };

    Assert.Throws<InvalidOperationException>(() => register.AddEmployee(duplicateEmployee));
}

[Test]
public void TestAddEmployees_DuplicateId_ThrowsException()
{
    var newEmployees = new List<Employee>
    {
        new OfficeWorker
        {
            Id = 6,
            Name = "Alicja",
            LastName = "Nowakowska",
            Age = 28,
            Experience = 6,
            Intellect = 130,
            Address = new Address { City = "Warszawa", Street = "Piekna", BuildingNr = 30, FlatNr = 6 }
        },
        new Trader
        {
            Id = 1,
            Name = "Michał",
            LastName = "Lewandowski",
            Age = 40,
            Experience = 12,
            Effectiveness = Effectiveness.Medium,
            CommissionAmount = 15,
            Address = new Address { City = "Kraków", Street = "Zielona", BuildingNr = 22, FlatNr = 4 }
        }
    };

    Assert.Throws<InvalidOperationException>(() => register.AddEmployees(newEmployees));
}


    }
}