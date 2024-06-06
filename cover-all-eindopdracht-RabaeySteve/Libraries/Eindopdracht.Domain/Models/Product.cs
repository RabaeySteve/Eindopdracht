using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EindopdrachtPG.Domain {
    #region Properties
    public partial class Product {
        public int Id { get; set; }
        public string NederlandseNaam { get; set; }
        public string WetenschappelijkeNaam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        #endregion

        public override string ToString() {
            return WetenschappelijkeNaam;
        }
    }
}
