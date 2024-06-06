using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Domain {
    public class ProductDTO {
        public int Id { get; set; }
        public string NederlandseNaam { get; set; }
        public string WetenschappelijkeNaam { get; set; }
        public string Beschrijving { get; set; }
        public int Aantal { get; set; }
        public decimal Prijs { get; set; }
    }

}
