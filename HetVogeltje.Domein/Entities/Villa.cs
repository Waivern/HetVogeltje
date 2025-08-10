using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HetVogeltje.Domein.Entities
{
    public  class Villa
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [Display(Name = "Omschrijving")]
        public string? Description { get; set; }
        [Display(Name = "Prijs per nacht")]
        [Range(10,10000)]
        public double Price { get; set; }
        public int Sqft { get; set; }
        [Range(1, 20)]
        public int Occupancy { get; set; }
        [Display(Name = "Afbeelding link")]
        public string? ImagePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get;set; }
    }
}
