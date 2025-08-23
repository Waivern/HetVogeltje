using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using Microsoft.AspNetCore.Mvc;

namespace HetVogeltje.Web.Controllers
{

    public class VillaNumberController : Controller
    {
        private readonly ApplicationDBContext _context;

        public VillaNumberController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villaNumber = _context.VillaNumbers.ToList();
            return View(villaNumber);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {
         if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("", "De naam en de omschrijving mogen niet hetzelfde zijn.");
            }
            if (ModelState.IsValid)
            {
                _context.Villas.Add(obj);
                _context.SaveChanges();
                TempData["Success"] = "Het toevoegen van de villa " + obj.Name + " is gelukt.";
                return RedirectToAction("Index", "Villa");
            }
            return View(obj);
        }

        //Edit
        public IActionResult Update(int villaId)
        {
            Villa? villaFromDb = _context.Villas.FirstOrDefault(u => u.Id == villaId);
            if (villaFromDb == null)
            {
                TempData["Error"] = "Er is iets misgegaan bij het aanpassen van de villa.";
                return RedirectToAction("Error", "Home");
            }
        
            return View(villaFromDb);

        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            
            if (ModelState.IsValid && obj.Id > 0)
            {
                _context.Villas.Update(obj);
                _context.SaveChanges();
                TempData["Success"] = "Het aanpassen van de villa " + obj.Name + " is gelukt.";
                return RedirectToAction("Index", "Villa");
            }
            return View(obj);
        }


        //  Delete

        public IActionResult Delete(int villaId)
        {
            Villa? villaFromDb = _context.Villas.FirstOrDefault(u => u.Id == villaId);
            if (villaFromDb is null)
            {
                TempData["Error"] = "Er is iets misgegaan bij het verwijderen van de villa.";
                return RedirectToAction("Error", "Home");
            }

            return View(villaFromDb);

        }
               
 
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villaFromDb = _context.Villas.FirstOrDefault(u => u.Id == obj.Id);
            if (villaFromDb is not null)
            {
                _context.Villas.Remove(villaFromDb) ;
                _context.SaveChanges();
                TempData["Success"] = "Villa succesvol verwijderd!";
                return RedirectToAction("Index", "Villa");
            }
            else
            {
                TempData["Error"] = "Er is iets misgegaan bij het verwijderen van de villa.";
                return RedirectToAction("Error", "Home");
            }
               
        }
    }
}
