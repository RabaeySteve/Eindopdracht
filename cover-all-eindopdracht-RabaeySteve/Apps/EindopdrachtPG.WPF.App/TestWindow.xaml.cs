using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows;

namespace EindopdrachtPG.WPF.App
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private readonly ILogger<TestWindow> _logger;
        private readonly IConfiguration _configuration; 

        public TestWindow(ILogger<TestWindow> logger, IConfiguration configuration)
        {
            InitializeComponent();

            _logger = logger;
            _configuration = configuration;
        }

        
    }
}
