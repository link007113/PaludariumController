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
using Xceed.Wpf.Toolkit;
namespace PaludariumController.Client.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ITemperatureService temperatureService;

        private readonly ILightsService lightsService;

        public MainWindow(ITemperatureService temperatureService, ILightsService lightsService)
        {
            InitializeComponent();
            this.temperatureService = temperatureService;
            this.lightsService = lightsService;
        }

        private async void TemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            TemperatureRequest result = await temperatureService.GetTempAsync();
            TemperatureText.Text = result.Temperature.ToString();
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (ClrPcker_Background.SelectedColor.HasValue)
            {
                Light light = new Light(System.Drawing.Color.FromArgb(ClrPcker_Background.SelectedColor.Value.R, ClrPcker_Background.SelectedColor.Value.G, ClrPcker_Background.SelectedColor.Value.B));
                LightRequest result = lightsService.SetLights(light, false);
            }
        }
    }
}
