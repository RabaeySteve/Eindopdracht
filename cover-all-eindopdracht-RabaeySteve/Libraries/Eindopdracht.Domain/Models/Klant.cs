using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Domain {
    public partial class Klant {

        #region Properties
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        #endregion

        #region Ctor
        public Klant() { }
        #endregion

        public override string ToString() {
            return $"{Naam} (Klantnummer: {Id}, Adres: {Adres})";
        }
    }
}
