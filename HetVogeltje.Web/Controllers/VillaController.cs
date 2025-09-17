using HetVogeltje.Application.Common.Interfaces;
using HetVogeltje.Domein.Entities;
using HetVogeltje.Infrastructuur.Data;
using Microsoft.AspNetCore.Mvc;

namespace HetVogeltje.Web.Controllers
{

    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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

                if (obj.Image != null)
                {
                    string filename= Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images\Villa");

                    using (var fileStream = new FileStream(Path.Combine(imagePath,filename),FileMode.Create))
                    {
                        obj.Image.CopyTo(fileStream);
                    }
                    obj.ImagePath = @"\Images\Villa\" + filename;
                }
                else
                {
                    obj.ImagePath = "https://placehold.co/600x404";
                }
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

                if (obj.Image != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images\Villa");


                    if(!string.IsNullOrEmpty(obj.ImagePath))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImagePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create))
                    {
                        obj.Image.CopyTo(fileStream);
                    }
                    obj.ImagePath = @"\Images\Villa\" + filename;
                }
            

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
                if (!string.IsNullOrEmpty(villaFromDb.ImagePath))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, villaFromDb.ImagePath.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
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
