using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetVogeltje.Domein.Entities
{
    public class Voorziening
    {
        [Key]
        public int Id { get; set; }
        public required string Naam { get; set; }
        [Display(Name = "Speciale bijzonderheden")]
        public string? SpecialeDetails { get; set; }
        // Foreign key relatie met de Villa entiteit
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        [ValidateNever]
        public Villa? Villa { get; set; }
       
    }
}
