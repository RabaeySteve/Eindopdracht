using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Data.SqlClient;
using EindopdrachtPG.Domain;
using EindopdrachtPG.Infrastructure;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows.Controls;



namespace EindopdrachtPG.WPF.App {
    public partial class MainWindow : Window {
        #region Properties
        private readonly ILogger<MainWindow>? _logger;
        private readonly TestWindow? _testWindow;
        private readonly EindopdrachtPGDb _repository;
        private readonly BestandInlezenWindow _bestandInlezenWindow;
        private readonly KlantenWindow _klantenWindow;
        private readonly OfferteBewerkenWindow _offerteBewerkenWindow;
        private readonly VerwijderenWindow _verwijderenWindow;
        private readonly LijstenWindow _lijstenWindow;

        private Klant geselecteerdeKlant;
        private List<OfferteProduct> offerteProducten = new List<OfferteProduct>();
        private List<ProductDTO> productDTOs = new List<ProductDTO>();
        #endregion
        #region Ctor
        public MainWindow(ILogger<MainWindow>? logger, TestWindow testWindow, EindopdrachtPGDb repository,
                          BestandInlezenWindow bestandInlezenWindow, KlantenWindow klantenWindow, OfferteBewerkenWindow offerteBewerkenWindow, VerwijderenWindow verwijderenWindow, LijstenWindow lijstenWindow
                         ) {
            InitializeComponent();

            _logger = logger;
            _testWindow = testWindow;
            _repository = repository;
            _bestandInlezenWindow = bestandInlezenWindow;
            _klantenWindow = klantenWindow;
            _offerteBewerkenWindow = offerteBewerkenWindow;
            _verwijderenWindow = verwijderenWindow;
            _lijstenWindow = lijstenWindow;
            
        }
        #endregion

        private void cbKlant_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            var comboBox = sender as ComboBox;
            if (comboBox != null) {
                string searchText = comboBox.Text.ToLower();
                var klanten = _repository.Klanten.Query.ZoekKlanten(searchText);
                comboBox.ItemsSource = klanten;

                comboBox.SelectionChanged += (s, ev) => {
                    geselecteerdeKlant = comboBox.SelectedItem as Klant;
                };
            }
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
                if (aantal <= 1000) {
                    productDTOs.Add(new ProductDTO {
                        Id = selectedProduct.Id,
                        NederlandseNaam = selectedProduct.NederlandseNaam,
                        WetenschappelijkeNaam = selectedProduct.WetenschappelijkeNaam,
                        Beschrijving = selectedProduct.Beschrijving,
                        Aantal = aantal,
                        Prijs = selectedProduct.Prijs * aantal
                    });

                    ProductDataGrid.ItemsSource = null;
                    ProductDataGrid.ItemsSource = productDTOs;
                } else {
                    MessageBox.Show("Het aantal mag niet groter zijn dan 1000.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } else {
                MessageBox.Show("Selecteer een product en voer een geldig aantal in.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void RemoveProductRow_Click(object sender, RoutedEventArgs e) {
            var selectedProductDTO = ProductDataGrid.SelectedItem as ProductDTO;
            if (selectedProductDTO != null) {
                productDTOs.Remove(selectedProductDTO);
                ProductDataGrid.ItemsSource = null;
                ProductDataGrid.ItemsSource = productDTOs;
                BerekenTotalePrijs();
            } else {
                MessageBox.Show("Selecteer een product om te verwijderen.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private decimal BerekenTotalePrijs() {
           
            decimal productKost = productDTOs.Sum(p => p.Prijs);

           
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



        private void btnOfferteAanmaken_Click(object sender, RoutedEventArgs e) {
            
            if (geselecteerdeKlant == null || productDTOs.Count == 0) {
                MessageBox.Show("Selecteer een klant en voeg minstens één product toe.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

           
            bool afhaal = chkAfhalen.IsChecked ?? false;
            bool levering = chkLeveren.IsChecked ?? false;
            bool aanleg = chkAanleg.IsChecked ?? false;

            if (!afhaal && !levering && !aanleg) {
                MessageBox.Show("Selecteer of de klant de bestelling afhaalt, laat leveren of laat installeren.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            DateTime datum = dpDatum.SelectedDate ?? DateTime.Now;

            
            Offerte nieuweOfferte = new Offerte {
                Datum = datum,
                KlantId = geselecteerdeKlant.Id,
                Afhaal = afhaal,
                Aanleg = aanleg,
                TotaalPrijs = BerekenTotalePrijs()
            };

            try {
                
                _repository.Offerte.Insert.NewRecord(nieuweOfferte);

                var nieuwToegevoegdeOffertes = _repository.Offerte.Query.GetLastOfferte();
                if (nieuwToegevoegdeOffertes == null || nieuwToegevoegdeOffertes.Count == 0) {
                    throw new Exception("De nieuw aangemaakte offerte kon niet worden gevonden.");
                }
                var nieuwToegevoegdeOfferte = nieuwToegevoegdeOffertes.First();

                
                List<OfferteProduct> offerteProducten = productDTOs.Select(dto => new OfferteProduct {
                    OfferteId = nieuwToegevoegdeOfferte.Id, 
                    ProductId = dto.Id,
                    Aantal = dto.Aantal
                }).ToList();

                
                foreach (var offerteProduct in offerteProducten) {
                    _repository.OfferteProduct.Insert.NewRecord(offerteProduct);
                }

                MessageBox.Show("Offerte succesvol aangemaakt!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch (Exception ex) {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}\n\nDetails:\n{ex.StackTrace}", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }






        private void SearchById_Click(object sender, RoutedEventArgs e) {
            if (int.TryParse(zoekOffertes.Text, out int id)) {
                var offertes = _repository.Offerte.Query.GetById(id);
                OfferteGrid.ItemsSource = offertes;

            } else {
                MessageBox.Show("Voer een geldige ID in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchByDate_Click(object sender, RoutedEventArgs e) {
            if (DateTime.TryParse(zoekOffertes.Text, out DateTime datum)) {
                var offertes = _repository.Offerte.Query.GetByDatum(datum);
                OfferteGrid.ItemsSource = offertes;
                
            } else {
                MessageBox.Show("Voer een geldige datum in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchByName_Click(object sender, RoutedEventArgs e) {
            try {
                if (geselecteerdeKlant == null) {
                    MessageBox.Show("Selecteer een klant.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int klantnummer = geselecteerdeKlant.Id;
                var offertes = _repository.Offerte.Query.GetAllByKlantId(klantnummer);
                OfferteGrid.ItemsSource = offertes;
            } catch (Exception ex) {
                MessageBox.Show($"Er is een fout opgetreden bij het ophalen van offertes: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Offerte_DoubleClick(object sender, RoutedEventArgs e) { }

        private void KlantenWindow_Click(object sender, RoutedEventArgs e) {
            _klantenWindow.Show();
            
        }
        private void LijstenWindow_Click(object sender, RoutedEventArgs e) {
            
            _lijstenWindow.Show();
            
        }
        
        private void BestandInlezen_Click(object sender, RoutedEventArgs e) {
            _bestandInlezenWindow?.Show();
            
        }

        private void ExitClick(object sender, RoutedEventArgs e) {
            //System.Environment.Exit(0); // we stoppen niet proper op deze manier met Generic Host
            Application.Current.Shutdown(); // ik neem huidige WPF app en ik vraag deze om te stoppen
        }

        private void Bereken_Click(object sender, RoutedEventArgs e) {
            BerekenTotalePrijs();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e) {
            var button = sender as Button;
            if (button != null) {
                var offerte = button.DataContext as Offerte;

                if (offerte != null) {
                    
                    _offerteBewerkenWindow.SetOfferte(offerte);

                    
                    _offerteBewerkenWindow.ShowDialog();
                }
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var offerteRow = OfferteGrid.SelectedItem as Offerte;
            var offerte = _repository.Offerte.Query.GetById(offerteRow.Id).Single();

           
            bool? result = _verwijderenWindow.ShowDialog();

            if (result == true) {
                
                _repository.OfferteProduct.Delete.DeleteAllesByOfferteId(offerte.Id);
               
                _repository.SaveChanges();
                MessageBox.Show("Row Deleted Successfully.");
            } else {
                
                MessageBox.Show("Verwijderen geannuleerd.");
            }



        }

       
    }
}
