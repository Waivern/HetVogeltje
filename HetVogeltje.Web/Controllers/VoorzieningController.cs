using HetVogeltje.Application.Common.Interfaces;
using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using HetVogeltje.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HetVogeltje.Web.Controllers
{

    public class VoorzieningController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VoorzieningController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var Voorziening = _unitOfWork.Voorziening.GetAll(includeProperties: "Villa");
            return View(Voorziening);
        }

        public IActionResult Create()
        {

            VoorzieningVM VoorzieningVM = new()
            {
                VoorzieningList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
         
            return View(VoorzieningVM); 
        }
        [HttpPost]
        public IActionResult Create(VoorzieningVM VoorzieningVM)
        {
        
            if (ModelState.IsValid)
            {
                _unitOfWork.Voorziening.Add(VoorzieningVM.Voorziening);
                _unitOfWork.Save();
                TempData["Success"] = "Het toevoegen van het huisnummer is gelukt.";
                return RedirectToAction(nameof(Index));
            }

            
            //Als het modelstate niet valid is, dan de lijst met villa's opnieuw ophalen
            VoorzieningVM.VoorzieningList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            //Vervolgens de view teruggeven met de ingevulde data van de viewmodel
            return View(VoorzieningVM);

        }
        
        

        //Edit
        public IActionResult Update(int VoorzieningId)
        {
            VoorzieningVM VoorzieningVM = new()
            {
                VoorzieningList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Voorziening = _unitOfWork.Voorziening.Get(u => u.Id == VoorzieningId)
            };
                      


            if (VoorzieningId == null || VoorzieningVM.Voorziening == null)
            {
                TempData["Error"] = "Er is iets misgegaan bij het openen van de locatie.";
                return RedirectToAction("Error", "Home");
            }
            return View(VoorzieningVM);

        }

        [HttpPost]
        public IActionResult Update(VoorzieningVM VoorzieningVM)
        {

           
            if (ModelState.IsValid)
            {
                _unitOfWork.Voorziening.Update(VoorzieningVM.Voorziening);
                _unitOfWork.Save();
                TempData["Success"] = "Het bijwerken van de locatie is gelukt.";
                return RedirectToAction(nameof(Index));
            }
                  
            //Als het modelstate niet valid is, dan de lijst met villa's opnieuw ophalen
            VoorzieningVM.VoorzieningList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            //Vervolgens de view teruggeven met de ingevulde data van de viewmodel
            return View(VoorzieningVM);

        }


        //  Delete

        public IActionResult Delete(int VoorzieningId)
        {
            VoorzieningVM VoorzieningVM = new()
            {
                VoorzieningList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Voorziening = _unitOfWork.Voorziening.Get(u => u.Id == VoorzieningId)
            };



            if (VoorzieningVM.Voorziening == null)
            {
                TempData["Error"] = "Er is iets misgegaan bij het openen van de locatie.";
                return RedirectToAction("Error", "Home");
            }
            return View(VoorzieningVM);

        }
               
 
        [HttpPost]
        public IActionResult Delete(VoorzieningVM VoorzieningVM)
        {
            Voorziening? VoorzieningfromDB = _unitOfWork.Voorziening.Get(u => u.Id == VoorzieningVM.Voorziening.Id);
            if (VoorzieningfromDB is not null)
            {
                _unitOfWork.Voorziening.Remove(VoorzieningfromDB) ;
                _unitOfWork.Save();
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
