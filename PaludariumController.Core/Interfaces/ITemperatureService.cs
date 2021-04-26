using PaludariumController.Core.Models;
using System.Threading.Tasks;

namespace PaludariumController.Core.Interfaces
{
   public interface ITemperatureService
    {
        Task<TemperatureRequest> GetTempAsync();
        CoolingRequest SetCooling(bool state);
        CoolingRequest GetCooling();

        public void SetInstanceName(string instanceName);
    }
}
