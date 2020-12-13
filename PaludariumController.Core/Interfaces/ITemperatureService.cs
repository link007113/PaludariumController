using PaludariumController.Core.Models;

namespace PaludariumController.Core.Interfaces
{
   public interface ITemperatureService
    {
        public TemperatureRequest GetTemp();

    }
}
