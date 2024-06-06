using System.ComponentModel;
using System.Windows;
using EindopdrachtPG.Infrastructure;
using Microsoft.Extensions.Logging;

namespace EindopdrachtPG.WPF.App {

    public partial class LijstenWindow : Window {
        #region Properties
        private readonly ILogger<LijstenWindow> _logger;
        private readonly EindopdrachtPGDb _repository;
        private readonly KlantenWindow _klantenWindow;
        private readonly BestandInlezenWindow _bestandInlezenWindow;
        #endregion
        #region Ctor
        public LijstenWindow(ILogger<LijstenWindow> logger, EindopdrachtPGDb repository,
                             KlantenWindow klantenWindow, BestandInlezenWindow bestandInlezenWindow) {
            InitializeComponent();
            _logger = logger;
            _repository = repository;
            _klantenWindow = klantenWindow;
            _bestandInlezenWindow = bestandInlezenWindow;

            LoadData();
        }
        #endregion

        private void LoadData() {
            OffertesDg.ItemsSource = _repository.Offerte.Query.GetAll();
            KlantenDg.ItemsSource = _repository.Klanten.Query.AllList();
            ProductenDg.ItemsSource = _repository.Producten.Query.All();
        }

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }

    }
}
