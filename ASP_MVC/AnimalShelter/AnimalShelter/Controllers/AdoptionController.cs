using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using AnimalShelter.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var adoptions = _context.Adoptions
                .Include(a => a.Animal)
                .Where(a => a.Animal.Status == StatusEnum.Adopted)
                .Select(a => new
                {
                    AnimalName = a.Animal.Name,
                    AdoptionDate = a.AdoptionDate
                })
                .ToList();

            // Prep data for taghelper controller
            var columns = new List<string> { "Animal Name", "Adoption Date" };
            var rows = adoptions.Select(a => new List<string>
            {
                a.AnimalName,
                a.AdoptionDate.ToShortDateString()
            }).ToList();

            var model = new { Columns = columns, Rows = rows };
            return View(model);
        }
    }
}
