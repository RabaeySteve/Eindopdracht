using EindopdrachtPG.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using EindopdrachtPG.Infrastructure.DataImport;
using System.Collections.Generic;
using EindopdrachtPG.Domain;
using System.ComponentModel;
namespace EindopdrachtPG.WPF.App {
    /// <summary>
    /// Interaction logic for BestandInlezen.xaml
    /// </summary>
    public partial class BestandInlezenWindow : Window {
        #region Properties
        private readonly ILogger<BestandInlezenWindow> _logger;
        private readonly IConfiguration _configuration;
        private readonly EindopdrachtPGDb _repository;
        private readonly string folderPath;
        #endregion

        #region Ctor
        public BestandInlezenWindow(ILogger<BestandInlezenWindow> logger, IConfiguration configuration, EindopdrachtPGDb repository) {
            InitializeComponent();

            _logger = logger;
            _configuration = configuration;
            _repository = repository;
           


            folderPath = GetDatabaseInputPath();

            if (!Directory.Exists(folderPath)) {
                MessageBox.Show($"De map {folderPath} kan niet gevonden worden. Controleer het pad.");
                return;
            }

            LoadFileList();
        }
        #endregion

        private string GetDatabaseInputPath() {
            
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = new DirectoryInfo(baseDirectory).Parent.Parent.Parent.Parent.Parent.FullName;
            return projectDirectory + @"\Database\Input\";
        }

        private void LoadFileList() {
            if (Directory.Exists(folderPath)) {
                var files = Directory.GetFiles(folderPath, "*.txt")
                                     .Select(f => new FileInfo(f).Name)
                                     .ToArray();
                Selector.ItemsSource = files;

                if (files.Any()) {
                    Selector.SelectedIndex = 0;
                }
            } else {
                MessageBox.Show($"De map {folderPath} is niet gevonden.");
            }
        }

        private void btnReadFile_Click(object sender, RoutedEventArgs e) {
            if (Selector.SelectedItem != null) {
                string selectedFile = Selector.SelectedItem.ToString();
                string filePath = folderPath + selectedFile; 

                try {
                    string fileContent;
                    using (StreamReader reader = new StreamReader(filePath)) {
                        fileContent = reader.ReadToEnd();
                    }
                    txtFileContent.Text = fileContent;
                } catch (IOException ex) {
                    MessageBox.Show($"Fout bij het lezen van het bestand: {ex.Message}");
                }
            } else {
                MessageBox.Show("Selecteer een bestand uit de lijst.");
            }
        }




        private void btnAddFile_Click(object sender, RoutedEventArgs e) {
            if (Selector.SelectedItem != null) {
                string selectedFile = Selector.SelectedItem.ToString();
                string filePath = Path.Combine(folderPath, selectedFile);

                string lowerFileName = selectedFile.ToLower();

                var dataImporter = new DataImporter();
                Dictionary<int, Klant> klantenDictionary = null; 

                try {
                    if (lowerFileName == "klanten.txt") {
                        klantenDictionary = dataImporter.ImportKlantenFromFile(filePath); 

                        foreach (var klant in klantenDictionary.Values) {
                            try {
                                _repository.Klanten.Insert.NewRecordIdentity(klant);
                            } catch (Exception ex) {
                                _logger.LogError($"Fout bij het toevoegen van klant: {klant.Naam}. Foutmelding: {ex.Message}");
                                MessageBox.Show($"Fout bij het toevoegen van klant: {klant.Naam}. Zie log voor details.");
                            }
                        }
                        MessageBox.Show("Klanten succesvol geïmporteerd en toegevoegd.");
                    } else if (lowerFileName == "producten.txt") {
                        var producten = dataImporter.ImportProductenFromFile(filePath);
                        foreach (var product in producten) {
                            try {
                                _repository.Producten.Insert.NewRecordIdentity(product);
                            } catch (Exception ex) {
                                _logger.LogError($"Fout bij het toevoegen van product: {product.NederlandseNaam}. Foutmelding: {ex.Message}");
                                MessageBox.Show($"Fout bij het toevoegen van product: {product.NederlandseNaam}. Zie log voor details.");
                            }
                        }
                        MessageBox.Show("Producten succesvol geïmporteerd en toegevoegd.");
                    } else if (lowerFileName == "offertes.txt") {
                        
                        

                        var offertes = dataImporter.ImportOffertesFromFile(filePath); 
                        foreach (var offerte in offertes) {
                            try {
                                _repository.Offerte.Insert.NewRecordIdentity(offerte);
                            } catch (Exception ex) {
                                _logger.LogError($"Fout bij het toevoegen van offerte ID: {offerte.Id}. Foutmelding: {ex.Message}");
                                MessageBox.Show($"Fout bij het toevoegen van offerte ID: {offerte.Id}. Zie log voor details.");
                            }
                        }
                        MessageBox.Show("Offertes succesvol geïmporteerd en toegevoegd.");
                    } else if (lowerFileName == "offerte_producten.txt") {
                        var offertes = _repository.Offerte.Query.GetAll();
                        var offerteProducten = dataImporter.ImportOfferteProductenFromFile(filePath, offertes, _repository.Producten.Query.GetProductPrijs);
                        foreach (var offerteProduct in offerteProducten) {
                            try {
                                _repository.OfferteProduct.Insert.NewRecord(offerteProduct);
                            } catch (Exception ex) {
                                _logger.LogError($"Fout bij het toevoegen van offerte product ID: {offerteProduct.OfferteId}. Foutmelding: {ex.Message}");
                                MessageBox.Show($"Fout bij het toevoegen van offerte product ID: {offerteProduct.OfferteId}. Zie log voor details.");
                            }
                        }

                        foreach (var offerte in offertes) {
                            try {
                                _repository.Offerte.Update.Update(offerte);
                            } catch (Exception ex) {
                                _logger.LogError($"Fout bij het updaten van offerte ID: {offerte.Id}. Foutmelding: {ex.Message}");
                                MessageBox.Show($"Fout bij het updaten van offerte ID: {offerte.Id}. Zie log voor details.");
                            }
                        }

                        MessageBox.Show("Offerte producten succesvol geïmporteerd en toegevoegd.");
                    }
                } catch (Exception ex) {
                    _logger.LogError($"Fout bij het importeren van gegevens uit het bestand: {ex.Message}");
                    MessageBox.Show($"Fout bij het importeren van gegevens. Zie log voor details.");
                }
            } else {
                MessageBox.Show("Selecteer een bestand uit de lijst.");
            }
        }






        
        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }


    }
}
