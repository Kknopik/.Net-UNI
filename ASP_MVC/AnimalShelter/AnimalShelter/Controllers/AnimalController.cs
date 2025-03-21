using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using AnimalShelter.Models;
using AnimalShelter.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnimalShelter.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animal
        [Authorize(Roles = "Admin,Manager,Doctor")]
        public IActionResult Index(string speciesFilter, string nameFilter)
        {

            var animals = _context.Animals.AsQueryable();

            // LINQ to filter
            if (!string.IsNullOrEmpty(speciesFilter) || !string.IsNullOrEmpty(nameFilter))
            {
                animals = animals.Where(a =>
                    (string.IsNullOrEmpty(speciesFilter) || a.Species.Contains(speciesFilter)) &&
                    (string.IsNullOrEmpty(nameFilter) || a.Name.Contains(nameFilter))
                );
            }

            // values -> filter
            ViewData["SpeciesFilter"] = speciesFilter;
            ViewData["NameFilter"] = nameFilter;

            return View(animals.ToList());
        }

        // GET: Animal/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Create([Bind("Id,Name,Species,Age,Status")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Animals.Add(animal);
                _context.SaveChanges();

                // Create adoption if animal adopted
                if (animal.Status == StatusEnum.Adopted)
                {
                    var adoption = new Adoption
                    {
                        AnimalId = animal.Id,
                        UserId = "admin" // Replace with logic later
                    };

                    // Adoption date auto set
                    _context.Adoptions.Add(adoption);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }

        // GET: Animal/Edit
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _context.Animals
                .Include(a => a.Adoption)
                .Include(a => a.MedicalRecords)
                .FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animal/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int id, [Bind("Id,Name,Species,Age,Status")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var exists = _context.Animals
                        .AsNoTracking()
                        .Any(a => a.Id == animal.Id);

                    if (!exists)
                    {
                        return NotFound();
                    }

                    if (animal.Status == StatusEnum.Adopted)
                    {
                        var adoption = new Adoption
                        {
                            AnimalId = animal.Id,
                            UserId = "user" // Replace later with proper logic
                        };

                        _context.Adoptions.Add(adoption);
                    }

                    _context.Attach(animal);
                    _context.Entry(animal).State = EntityState.Modified;

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Animals.Any(a => a.Id == animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }

        // GET: Animal/Delete
        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _context.Animals
                .Include(a => a.Adoption)
                .Include(a => a.MedicalRecords)
                .FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animal/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteConfirmed(int id)
        {
            var animal = _context.Animals
                .Include(a => a.Adoption)
                .Include(a => a.MedicalRecords)
                .FirstOrDefault(a => a.Id == id);

            if (animal != null)
            {
                if (animal.Adoption != null)
                {
                    _context.Adoptions.Remove(animal.Adoption);
                }

                if (animal.MedicalRecords != null && animal.MedicalRecords.Any())
                {
                    _context.MedicalRecords.RemoveRange(animal.MedicalRecords);
                }

                _context.Animals.Remove(animal);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Animal/Available
        public IActionResult Available()
        {
            var availableAnimals = _context.Animals
                                            .Where(a => a.Status == StatusEnum.Available)
                                            .ToList();

            return View(availableAnimals);
        }

        // POST: Animal/Reserve
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public IActionResult Reserve(int id)
        {
            var animal = _context.Animals.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            animal.Status = StatusEnum.Reserved;
            _context.SaveChanges();

            return RedirectToAction(nameof(Available));
        }
    }
}
