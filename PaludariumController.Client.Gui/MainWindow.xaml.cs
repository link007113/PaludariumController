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
        private bool checkbox_doFade;
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

        private void Fade_Checked(object sender, RoutedEventArgs e)
        {
            checkbox_doFade = true;
        }

        private void Fade_Unchecked(object sender, RoutedEventArgs e)
        {
            checkbox_doFade = false;
        }

        private void LightColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (LightColor.SelectedColor.HasValue)
            {
                Light light = new Light(System.Drawing.Color.FromArgb(LightColor.SelectedColor.Value.R, LightColor.SelectedColor.Value.G, LightColor.SelectedColor.Value.B));
                LightRequest result = lightsService.SetLights(light, checkbox_doFade);
            }
        }
        //private void LightColor_SelectedColorChanged(RoutedPropertyChangedEventHandler<Color?> e)
        //{
        //    if (LightColor.SelectedColor.HasValue)
        //    {
        //        Light light = new Light(System.Drawing.Color.FromArgb(LightColor.SelectedColor.Value.R, LightColor.SelectedColor.Value.G, LightColor.SelectedColor.Value.B));
        //        LightRequest result = lightsService.SetLights(light, false);
        //    }
        //}
    }
}
