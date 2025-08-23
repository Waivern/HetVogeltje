using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HetVogeltje.Domein.Entities
{
    public class VillaNumber
    {
        // Hier gebruiken we DatabaseGeneratedOption.None omdat we willen dat de Villa_Number handmatig wordt ingesteld en niet automatisch door de database wordt gegenereerd.
        // Dit is belangrijk omdat Villa_Number waarschijnlijk een uniek identificatienummer is dat door de gebruiker of het systeem wordt beheerd.
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Villa_Number { get; set; }

        // Foreign key relatie met de Villa entiteit
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa Villa { get; set; }
        public string? SpecialeDetails { get; set; }
    }
}
