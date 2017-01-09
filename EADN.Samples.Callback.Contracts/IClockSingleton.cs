using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace EADN.Samples.Callback.Contracts
{
    [ServiceContract(Name = "ClockSingleton",  Namespace ="EADN.Samples.Callback.Contracts",
        CallbackContract = typeof(ITimerCallback))]
    public interface IClockSingleton
    {
        [OperationContract]
        void RegisterClient();
    }
}
