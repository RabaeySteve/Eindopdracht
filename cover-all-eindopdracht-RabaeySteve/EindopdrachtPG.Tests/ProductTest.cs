using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Tests {
    public class ProductTests {
        [Fact]
        public void Product_ToString_ReturnsWetenschappelijkeNaam() {
            
            var product = new Product {
                Id = 1,
                NederlandseNaam = "Roos",
                WetenschappelijkeNaam = "Rosa",
                Beschrijving = "Een mooie roos",
                Prijs = 9.99m
            };

            
            var result = product.ToString();

            
            Assert.Equal("Rosa", result);
        }

        [Fact]
        public void Product_CanSetProperties() {
            
            var product = new Product();

            product.Id = 1;
            product.NederlandseNaam = "Roos";
            product.WetenschappelijkeNaam = "Rosa";
            product.Beschrijving = "Een mooie roos";
            product.Prijs = 9.99m;

            
            Assert.Equal(1, product.Id);
            Assert.Equal("Roos", product.NederlandseNaam);
            Assert.Equal("Rosa", product.WetenschappelijkeNaam);
            Assert.Equal("Een mooie roos", product.Beschrijving);
            Assert.Equal(9.99m, product.Prijs);
        }
    }
}
