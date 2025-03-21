using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;
using AnimalShelter.Data;
using System.Linq;

namespace AnimalShelter.Controllers
{
    public class MedicalRecordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedicalRecord
        [Authorize(Roles = "Admin,Manager,Doctor")]
        public IActionResult Index(int? animalId, DateTime? startDate)
        {

            var medicalRecords = _context.MedicalRecords.Include(mr => mr.Animal).AsQueryable();

            if (animalId.HasValue)
            {
                medicalRecords = medicalRecords.Where(mr => mr.AnimalId == animalId);
            }

            if (startDate.HasValue)
            {
                medicalRecords = medicalRecords.Where(mr => mr.RecordDate >= startDate);
            }

            return View(medicalRecords.ToList());
        }

        // GET: MedicalRecord/Create
        [Authorize(Roles = "Doctor")]
        public IActionResult Create()
        {
            // Animal list
            ViewBag.Animals = _context.Animals.ToList();
            return View();
        }

        // POST: MedicalRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public IActionResult Create([Bind("AnimalId,RecordDate,Description")] MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalRecord);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // If error -> reload
            ViewBag.Animals = _context.Animals.ToList();
            return View(medicalRecord);
        }

        // GET: MedicalRecord/Edit
        [Authorize(Roles = "Doctor")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = _context.MedicalRecords.Find(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            ViewBag.Animals = _context.Animals.ToList();
            return View(medicalRecord);
        }

        // POST: MedicalRecord/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public IActionResult Edit(int id, [Bind("Id,AnimalId,RecordDate,Description")] MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalRecord);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MedicalRecords.Any(mr => mr.Id == medicalRecord.Id))
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

            ViewBag.Animals = _context.Animals.ToList();
            return View(medicalRecord);
        }

        // GET: MedicalRecord/Delete
        [Authorize(Roles = "Doctor")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = _context.MedicalRecords
                .Include(mr => mr.Animal)
                .FirstOrDefault(mr => mr.Id == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        // POST: MedicalRecord/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public IActionResult DeleteConfirmed(int id)
        {
            var medicalRecord = _context.MedicalRecords.Find(id);
            if (medicalRecord != null)
            {
                _context.MedicalRecords.Remove(medicalRecord);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MedicalRecord/Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = _context.MedicalRecords
                .Include(mr => mr.Animal)
                .FirstOrDefault(mr => mr.Id == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }


    }
}
