using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EADN.Samples.Callback.Contracts
{
    [ServiceContract(Name = "ITimerCallback", Namespace = "EADN.Samples.Callback.Contracts")]
    public interface ITimerCallback
    {
        [OperationContract]
        void ServerCallback(object sender, TimeServerEventArgument argument);
    }
}
