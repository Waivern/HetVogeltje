using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using HetVogeltje.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var villaNumber = _context.VillaNumbers.Include(u=>u.Villa).ToList();
            return View(villaNumber);
        }

        public IActionResult Create()
        {

            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
         
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM villaNumberVM)
        {
            //Controle of het huisnummer al bestaat
            bool HuisnummerBestaat = _context.VillaNumbers.Any(u => u.Villa_Number == villaNumberVM.Huisnummer.Villa_Number);


            //Andere methode om te controleren of het huisnummer al bestaat
            //if (_context.VillaNumbers.Any(u => u.Villa_Number == villaNumberVM.Huisnummer.Villa_Number))
            //{
            //    TempData["Error"] = "Huisnummer: " + villaNumberVM.Huisnummer.Villa_Number + " bestaat al, aanmaken is niet gelukt.";
            //    return RedirectToAction("Index");
            //}


            //  ModelState.Remove("VillaNumber.Villa");// deze hoeft niet, omdat hij in de entity al niet wordt gecontroleerd.
            if (ModelState.IsValid && !HuisnummerBestaat)
            {
                _context.VillaNumbers.Add(villaNumberVM.Huisnummer);
                _context.SaveChanges();
                TempData["Success"] = "Het toevoegen van het huisnummer is gelukt.";
                return RedirectToAction("Index");
            }

            if(HuisnummerBestaat)
            {
                TempData["Error"] = "Huisnummer: " + villaNumberVM.Huisnummer.Villa_Number + " bestaat al, aanmaken is niet gelukt.";            
            };
            //Als het modelstate niet valid is, dan de lijst met villa's opnieuw ophalen
            villaNumberVM.VillaList = _context.Villas.ToList().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            //Vervolgens de view teruggeven met de ingevulde data van de viewmodel
            return View(villaNumberVM);

        }
        
        

        //Edit
        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Huisnummer = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };
                      


            if (villaNumberId == null || villaNumberVM.Huisnummer == null)
            {
                TempData["Error"] = "Er is iets misgegaan bij het openen van de locatie.";
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);

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
