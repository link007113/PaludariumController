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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaludariumController.Core.Models;
using PaludariumController.Core.Interfaces;
using PaludariumController.Client.InfraStructure;

namespace PaludariumController.Client.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ITemperatureService temperatureService;
        public MainWindow()
        { }
        public MainWindow(ITemperatureService temperatureService)
        {
            InitializeComponent();
            this.temperatureService = temperatureService;
        }

        private  async void TemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            TemperatureRequest result = await temperatureService.GetTempAsync();
            TemperatureText.Text = result.Temperature.ToString();
        }
    }
}
