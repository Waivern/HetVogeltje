using HetVogeltje.Domein.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HetVogeltje.Web.ViewModels
{
    public class VoorzieningVM
    {
        public Voorziening? Voorziening { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? VoorzieningList { get; set; }
    }
}
