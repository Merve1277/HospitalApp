using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Patients
        public IActionResult Index()
        {
            var patients = _context.Patients.ToList(); // veritabanından çekiyoruz
            return View(patients);
        }

        // GET: /Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges(); // veritabanına kaydet
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: /Patients/Edit/1
        public IActionResult Edit(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();

            return View(patient);
        }

        // POST: /Patients/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient updatedPatient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Update(updatedPatient);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(updatedPatient);
        }

        // POST: /Patients/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Patients/Dashboard
        public IActionResult Dashboard()
        {
            var patients = _context.Patients.ToList();

            var totalPatients = patients.Count;
            var averageAge = patients.Count > 0 ? patients.Average(p => p.Age) : 0;
            var lastPatient = patients.OrderByDescending(p => p.RegisterDate).FirstOrDefault();
            var mostCommonDisease = patients
                .GroupBy(p => p.Disease)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            ViewBag.TotalPatients = totalPatients;
            ViewBag.AverageAge = Math.Round(averageAge, 1);
            ViewBag.LastPatient = lastPatient?.Name ?? "Yok";
            ViewBag.MostCommonDisease = mostCommonDisease ?? "Yok";

            return View();
        }
    }
}
