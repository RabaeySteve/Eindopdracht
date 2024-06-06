using EindopdrachtPG.Infrastructure;
using EindopdrachtPG.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EindopdrachtPG.WPF.App {
    
    public partial class KlantenWindow : Window {
        #region Properties
        private readonly ILogger<KlantenWindow> _logger;
        private readonly IConfiguration _configuration;
        private readonly EindopdrachtPGDb _repository;

        private Klant geselecteerdeKlant;

        #endregion

        #region Ctor
        public KlantenWindow(ILogger<KlantenWindow> logger, IConfiguration configuration, EindopdrachtPGDb repository) {
            InitializeComponent();

            _logger = logger;
            _configuration = configuration;
            _repository = repository;



        }
        #endregion

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }

        private void Zoek_Click(object sender, RoutedEventArgs e) {
            try {
                if (geselecteerdeKlant == null) {
                    MessageBox.Show("Selecteer een klant.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int klantnummer = geselecteerdeKlant.Id;
                var offertes = _repository.Offerte.Query.GetAllByKlantId(klantnummer);
                OfferteDataGrid.ItemsSource = offertes;
                txtKlantnummer.Text = klantnummer.ToString();
                txtNaam.Text = geselecteerdeKlant.Naam;
                txtAdres.Text = geselecteerdeKlant.Adres;
                txtAantalOffertes.Text = offertes.Count.ToString();
            } catch (Exception ex) {
                MessageBox.Show($"Er is een fout opgetreden bij het ophalen van offertes: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


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


    }
}
