using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for VerwijderenWindow.xaml
    /// </summary>
    public partial class VerwijderenWindow : Window {
        private readonly ILogger<VerwijderenWindow> _logger;
        private readonly IConfiguration _configuration;

        public VerwijderenWindow(ILogger<VerwijderenWindow> logger, IConfiguration configuration) {
            InitializeComponent();

            _logger = logger;
            _configuration = configuration;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            _logger.LogDebug("Closing");
            _logger.LogInformation("Connection string: " + _configuration.GetConnectionString("Db"));

            this.Visibility = Visibility.Collapsed; // om te verbergen
            e.Cancel = true; // om te voorkomen dat de WPF app het window toch nog vernietigt
        }
        public void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false; 
        }
        public void YesButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true; 
        }
    }
}
