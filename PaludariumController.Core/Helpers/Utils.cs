using System.Threading;

namespace PaludariumController.Helpers
{
    class Utils
    {
        public static void Wait(string reason, int timeOut)
        {
            if (reason == null) { LogHelper.Log($"Program sleeping for {timeOut} seconds", "info"); }
            else { LogHelper.Log(reason, "info"); }

            Thread.Sleep(timeOut * 1000);
        }
        public static void Wait(string reason)
        {
            Wait(reason, 2);
        }
    }
}
