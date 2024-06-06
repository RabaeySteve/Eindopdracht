using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using EindopdrachtPG.Domain;
using EindopdrachtPG.Infrastructure;
using Microsoft.Extensions.Logging;

using System.Windows.Controls;
using System.ComponentModel;

namespace EindopdrachtPG.WPF.App {
    public partial class OfferteBewerkenWindow : Window {

        #region Properties
        private readonly ILogger<OfferteBewerkenWindow>? _logger;
        private readonly EindopdrachtPGDb _repository;
        public Offerte _offerte { get; set; }
        private List<ProductDTO> _productDTOs = new List<ProductDTO>();
        private Dictionary<int, Klant> klantenDictionary = new Dictionary<int, Klant>(); // Voeg klantenDictionary toe
        #endregion

        #region Ctor
        public OfferteBewerkenWindow(ILogger<OfferteBewerkenWindow>? logger, EindopdrachtPGDb repository) {
            InitializeComponent();
            _logger = logger;
            _repository = repository;
        }
        #endregion

        public void SetOfferte(Offerte offerte) {
            _offerte = offerte;
            LoadOfferteDetails();
        }

        private void LoadOfferteDetails() {
            if (_offerte == null) {
                _logger.LogError("Offerte is null.");
                MessageBox.Show("Offerte is null.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Klant klant = null;

                
            klant = _repository.Klanten.Query.GetById(_offerte.KlantId);
                

            if (klant != null) {
                tbKlant.Text = klant.Naam;
            } else {
                _logger.LogError($"Klant met ID {_offerte.KlantId} niet gevonden.");
                tbKlant.Text = "Onbekende klant";
            }

            dpDatum.SelectedDate = _offerte.Datum;
            chkAfhalen.IsChecked = _offerte.Afhaal;
            chkLeveren.IsChecked = !_offerte.Afhaal;
            chkAanleg.IsChecked = _offerte.Aanleg;

            _productDTOs = new List<ProductDTO>();

            var offerteProducten = _repository.OfferteProduct.Query.GetByOfferteId(_offerte.Id);
            foreach (var op in offerteProducten) {
                var product = _repository.Producten.Query.GetProductById(op.ProductId).FirstOrDefault();
                if (product != null) {
                    _productDTOs.Add(new ProductDTO {
                        Id = op.ProductId,
                        NederlandseNaam = product.NederlandseNaam,
                        WetenschappelijkeNaam = product.WetenschappelijkeNaam,
                        Beschrijving = product.Beschrijving,
                        Aantal = op.Aantal,
                        Prijs = product.Prijs * op.Aantal
                    });
                } else {
                    _logger.LogError($"Product met ID {op.ProductId} niet gevonden.");
                }
            }

            ProductDataGrid.ItemsSource = _productDTOs;
            BerekenTotalePrijs();
        }


        private void cbProduct_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            var comboBox = sender as ComboBox;
            if (comboBox != null) {
                string searchText = comboBox.Text.ToLower();
                var producten = _repository.Producten.Query.ZoekProducten(searchText);
                comboBox.ItemsSource = producten;
            }
        }

        private void AddProductRow_Click(object sender, RoutedEventArgs e) {
            var selectedProduct = cbProduct.SelectedItem as Product;
            if (selectedProduct != null && int.TryParse(tbAantal.Text, out int aantal)) {
                _productDTOs.Add(new ProductDTO {
                    Id = selectedProduct.Id,
                    NederlandseNaam = selectedProduct.NederlandseNaam,
                    WetenschappelijkeNaam = selectedProduct.WetenschappelijkeNaam,
                    Beschrijving = selectedProduct.Beschrijving,
                    Aantal = aantal,
                    Prijs = selectedProduct.Prijs * aantal
                });

                ProductDataGrid.ItemsSource = null;
                ProductDataGrid.ItemsSource = _productDTOs;
            } else {
                MessageBox.Show("Selecteer een product en voer een geldig aantal in.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveProductRow_Click(object sender, RoutedEventArgs e) {
            var selectedProductDTO = ProductDataGrid.SelectedItem as ProductDTO;
            if (selectedProductDTO != null) {
                _productDTOs.Remove(selectedProductDTO);
                ProductDataGrid.ItemsSource = null;
                ProductDataGrid.ItemsSource = _productDTOs;
                BerekenTotalePrijs();
            } else {
                MessageBox.Show("Selecteer een product om te verwijderen.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Bereken_Click(object sender, RoutedEventArgs e) {
            BerekenTotalePrijs();
        }

        private decimal BerekenTotalePrijs() {
            
            decimal productKost = _productDTOs.Sum(p => p.Prijs);


            if (productKost > 5000) {
                productKost *= 0.90m;
            } else if (productKost > 2000) {
                productKost *= 0.95m;
            }

            decimal totalePrijs = productKost;


            bool leveringNodig = chkLeveren.IsChecked == true || chkAanleg.IsChecked == true;
            if (leveringNodig) {
                if (productKost < 500) {
                    totalePrijs += 100;
                } else if (productKost < 1000) {
                    totalePrijs += 50;
                }

            }


            if (chkAanleg.IsChecked == true) {
                if (productKost > 5000) {
                    totalePrijs += productKost * 0.05m;
                } else if (productKost > 2000) {
                    totalePrijs += productKost * 0.10m;
                } else {
                    totalePrijs += productKost * 0.15m;
                }
            }


            tbTotalePrijs.Text = totalePrijs.ToString("C"); 

            return totalePrijs;
        }

        private void btnWijzigingenOpslaan_Click(object sender, RoutedEventArgs e) {
            bool afhaal = chkAfhalen.IsChecked ?? false;
            bool levering = chkLeveren.IsChecked ?? false;
            bool aanleg = chkAanleg.IsChecked ?? false;

            if (!afhaal && !levering && !aanleg) {
                MessageBox.Show("Selecteer of de klant de bestelling afhaalt, laat leveren of laat installeren.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            var bijgewerkteOfferteProducten = _productDTOs.Select(dto => new OfferteProduct {
                OfferteId = _offerte.Id,
                ProductId = dto.Id,
                Aantal = dto.Aantal
            }).ToList();

            _offerte.Datum = dpDatum.SelectedDate ?? _offerte.Datum;
            _offerte.Afhaal = afhaal;
            _offerte.Aanleg = aanleg;
            _offerte.TotaalPrijs = BerekenTotalePrijs();

            try {
                
                _repository.OfferteProduct.Delete.DeleteByOfferteId(_offerte.Id);

                
                foreach (var offerteProduct in bijgewerkteOfferteProducten) {
                    _repository.OfferteProduct.Insert.NewRecord(offerteProduct);
                }

                
                _repository.Offerte.Update.Update(_offerte);

                MessageBox.Show("Wijzigingen succesvol opgeslagen!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            } catch (Exception ex) {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}\n\nDetails:\n{ex.StackTrace}", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }
    }
}
