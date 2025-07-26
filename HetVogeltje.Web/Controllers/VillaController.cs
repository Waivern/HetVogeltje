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
    }
}
