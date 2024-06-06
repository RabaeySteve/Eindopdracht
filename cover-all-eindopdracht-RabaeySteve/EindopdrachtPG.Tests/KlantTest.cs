using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Tests {
    public class KlantTests {
        [Fact]
        public void Klant_ToString_ReturnsNaam() {
            
            var klant = new Klant {
                Id = 1,
                Naam = "Jan Jansen",
                Adres = "Hoofdstraat 1, 1234 AB Amsterdam"
            };

            
            var result = klant.ToString();

            
            Assert.Equal("Jan Jansen", result);
        }

        [Fact]
        public void Klant_CanSetProperties() {
            
            var klant = new Klant();

            
            klant.Id = 1;
            klant.Naam = "Jan Jansen";
            klant.Adres = "Hoofdstraat 1, 1234 AB Amsterdam";

            
            Assert.Equal(1, klant.Id);
            Assert.Equal("Jan Jansen", klant.Naam);
            Assert.Equal("Hoofdstraat 1, 1234 AB Amsterdam", klant.Adres);
        }
    }
}
