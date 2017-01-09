using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EADN.Samples.Callback.Contracts
{
    // Client Implementation in Contracts -> Verfügbar an alle möglichen Clients
    [CallbackBehavior(UseSynchronizationContext =true)]
    public class TimerCallback : ITimerCallback
    {
        public event EventHandler<TimeServerEventArgument> Tick;
        public void ServerCallback(object sender, TimeServerEventArgument argument)
        {
            Tick?.Invoke(this, argument);
            Console.WriteLine($"Callback received: {argument}");
        }
    }
}
