using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using Microsoft.AspNetCore.Mvc;

namespace HetVogeltje.Web.Controllers
{

    public class VillaController : Controller
    {
        private readonly ApplicationDBContext _context;

        public VillaController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
            return View(villas);
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
                return RedirectToAction("Error", "Home");
            }
        
            return View(villaFromDb);

        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("", "De naam en de omschrijving mogen niet hetzelfde zijn.");
            }
            if (ModelState.IsValid)
            {
                _context.Villas.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View(obj);
        }


        //  Delete
        public IActionResult Delete(int villaId)
        {
                       var villaFromDb = _context.Villas.FirstOrDefault(u => u.Id == villaId);
            if (villaFromDb == null)
            {
                return RedirectToAction("Error", "Home");
            }
                _context.Villas.Remove(villaFromDb);
            _context.SaveChanges();
            return RedirectToAction("Index", "Villa");
        }

    }
}
