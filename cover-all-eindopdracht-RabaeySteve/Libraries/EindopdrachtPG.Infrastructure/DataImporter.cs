using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EindopdrachtPG.Domain;

namespace EindopdrachtPG.Infrastructure.DataImport {
    public class DataImporter {
        private List<Product> _producten;

        public DataImporter() {
            _producten = new List<Product>();
        }
        #region ImportKlant
        public Dictionary<int, Klant> ImportKlantenFromFile(string filePath) {
            var klanten = new Dictionary<int, Klant>();

            foreach (var line in File.ReadLines(filePath)) {
                var klant = ParseKlant(line);
                if (klant != null) {
                    klanten[klant.Id] = klant; 
                }
            }

            return klanten;
        }


        private Klant ParseKlant(string line) {
            var parts = line.Split('|');
            if (parts.Length != 3) {
                Console.WriteLine($"Niet geldig op lijn: {line}");
                return null;
            }

            return new Klant {
                Id = int.Parse(parts[0]),
                Naam = parts[1],
                Adres = parts[2]
            };
        }
        #endregion

        #region ImportProducts


        public List<Product> ImportProductenFromFile(string filePath) {
            _producten.Clear();

            foreach (var line in File.ReadLines(filePath)) {
                var product = ParseProduct(line);
                if (product != null) {
                    _producten.Add(product);
                }
            }

            return _producten;
        }

        private Product ParseProduct(string line) {
            var parts = line.Split('|');
            if (parts.Length != 5) {
                Console.WriteLine($"Niet geldig op lijn: {line}");
                return null;
            }

            try {
                return new Product {
                    Id = int.Parse(parts[0]),
                    NederlandseNaam = parts[1],
                    WetenschappelijkeNaam = parts[2],
                    Prijs = decimal.Parse(parts[3]),
                    Beschrijving = parts[4]
                };
            } catch (Exception ex) {
                Console.WriteLine($"Fout bij het parsen van lijn: {line}. Fout: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region ImportOffertes
        public List<Offerte> ImportOffertesFromFile(string filePath/*, Dictionary<int, Klant> klantenDictionary*/) {
            var offertes = new List<Offerte>();

            foreach (var line in File.ReadLines(filePath)) {
                var offerte = ParseOfferte(line);
                if (offerte != null) {
                   
                        offertes.Add(offerte);
                    
                }
            }

            return offertes;
        }

        private Offerte ParseOfferte(string line) {
            var parts = line.Split('|').Select(p => p.Trim()).ToArray(); 

            try {
                return new Offerte {
                    Id = int.Parse(parts[0]),
                    Datum = DateTime.Parse(parts[1]),
                    KlantId = int.Parse(parts[2]),
                    Afhaal = bool.Parse(parts[3]),
                    Aanleg = bool.Parse(parts[4]),
                    OfferteProducten = new List<OfferteProduct>() 
                };
            } catch (Exception ex) {
                Console.WriteLine($"Fout bij het parsen van lijn: {line}. Fout: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region ImportOfferteProducten
        public List<OfferteProduct> ImportOfferteProductenFromFile(string filePath, List<Offerte> offertes, Func<int, decimal> getProductPrijs) {
            var offerteProducten = new List<OfferteProduct>();

            foreach (var line in File.ReadLines(filePath)) {
                var offerteProduct = ParseOfferteProduct(line);
                if (offerteProduct != null) {
                    var offerte = offertes.FirstOrDefault(o => o.Id == offerteProduct.OfferteId);
                    if (offerte != null) {
                        offerte.OfferteProducten.Add(offerteProduct);
                        offerteProducten.Add(offerteProduct);
                    } else {
                        Console.WriteLine($"Geen offerte gevonden met Id: {offerteProduct.OfferteId}");
                    }
                }
            }

            
            foreach (var offerte in offertes) {
                offerte.BerekenTotalePrijs(getProductPrijs);
            }

            return offerteProducten;
        }

        private OfferteProduct ParseOfferteProduct(string line) {
            var parts = line.Split('|').Select(p => p.Trim()).ToArray();
            if (parts.Length != 3) {
                Console.WriteLine($"Niet geldig op lijn: {line}");
                return null;
            }

            return new OfferteProduct {
                OfferteId = int.Parse(parts[0]),
                ProductId = int.Parse(parts[1]),
                Aantal = int.Parse(parts[2])
            };
        }
        #endregion

    }
}
