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
                return RedirectToAction(nameof(Index));
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
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {

           
            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Update(villaNumberVM.Huisnummer);
                _context.SaveChanges();
                TempData["Success"] = "Het bijwerken van de locatie is gelukt.";
                return RedirectToAction(nameof(Index));
            }
                  
            //Als het modelstate niet valid is, dan de lijst met villa's opnieuw ophalen
            villaNumberVM.VillaList = _context.Villas.ToList().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            //Vervolgens de view teruggeven met de ingevulde data van de viewmodel
            return View(villaNumberVM);

        }


        //  Delete

        public IActionResult Delete(int villaNumberId)
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



            if (villaNumberVM.Huisnummer == null)
            {
                TempData["Error"] = "Er is iets misgegaan bij het openen van de locatie.";
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);

        }
               
 
        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? VillaNumberfromDB = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberVM.Huisnummer.Villa_Number);
            if (VillaNumberfromDB is not null)
            {
                _context.VillaNumbers.Remove(VillaNumberfromDB) ;
                _context.SaveChanges();
                TempData["Success"] = "Huisnummer succesvol verwijderd!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "Er is iets misgegaan bij het verwijderen van het huisnummer.";
                return View();
            }
               
        }
    }
}
