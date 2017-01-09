using EADN.Samples.Callback.Implementation;
using EADN.Samples.Callback.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EADN.Samples.Callback.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost clockService;

            try
            {
                clockService = new ServiceHost(typeof(ClockSingleton));
                clockService.AddServiceEndpoint(
                    typeof(IClockSingleton),
                    new NetTcpBinding(),
                    "net.tcp://localhost:4715/Clock");
                clockService.Open();

                Console.WriteLine("Host: Clock is running");
                Console.ReadKey();
            }
            catch ( Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }
    }
}
