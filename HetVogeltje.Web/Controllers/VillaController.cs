using HetVogeltje.Application.Common.Interfaces;
using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using Microsoft.AspNetCore.Mvc;

namespace HetVogeltje.Web.Controllers
{

    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
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
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Het toevoegen van de villa " + obj.Name + " is gelukt.";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        //Edit
        public IActionResult Update(int villaId)
        {
            Villa? villaFromDb = _unitOfWork.Villa.Get(u => u.Id == villaId);
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
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Het aanpassen van de villa " + obj.Name + " is gelukt.";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }


        //  Delete

        public IActionResult Delete(int villaId)
        {
            Villa? villaFromDb = _unitOfWork.Villa.Get(u => u.Id == villaId);
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
            Villa? villaFromDb = _unitOfWork.Villa.Get(u => u.Id == obj.Id);
            if (villaFromDb is not null)
            {
                _unitOfWork.Villa.Remove(villaFromDb) ;
                _unitOfWork.Save();
                TempData["Success"] = "Villa succesvol verwijderd!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "Er is iets misgegaan bij het verwijderen van de villa.";
                return RedirectToAction("Error", "Home");
            }
               
        }
    }
}
