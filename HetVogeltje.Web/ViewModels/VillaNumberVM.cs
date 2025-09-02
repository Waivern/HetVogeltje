using HetVogeltje.Domein.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HetVogeltje.Web.ViewModels
{
    public class VillaNumberVM
    {
        public VillaNumber? Huisnummer { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? VillaList { get; set; }
    }
}
