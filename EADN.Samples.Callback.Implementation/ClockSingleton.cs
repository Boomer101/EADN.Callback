using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EADN.Samples.Callback.Contracts;
using System.ServiceModel;
using System.Timers;

namespace EADN.Samples.Callback.Implementation
{
    [ServiceBehavior  ]
    public class ClockSingleton : IClockSingleton
    {
        // Lock
        private readonly object SyncRoot = new object();

        // Mehrere Clients registrieren ermöglichen
        private List<ITimerCallback> ClientCallbacks = new List<ITimerCallback>();

        public ClockSingleton()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeServerEventArgument argument = new TimeServerEventArgument(DateTime.Now);

            // Crude, but works
            lock (SyncRoot)
            {
                // Temp. Listen-Kopie erzeugen mit ToList() und diese abarbeiten
                foreach (ITimerCallback proxy in ClientCallbacks.ToList())
                {
                    try
                    {
                        proxy.ServerCallback(Environment.MachineName, argument);
                    }
                    catch
                    {
                        // Fehlerhafte aus Original Liste löschen
                        ClientCallbacks.Remove(proxy); // Eigenes lock für diese Methode auch möglich

                        // Weitere Möglichkeit: 
                        // 2. Liste mit Fehlerhaften Clients aufbauen, nachher aus Orig.-Liste löschen
                    }
                }
            }
        }
        public void RegisterClient()
        {
            lock (SyncRoot)
            {
                // Achtung, kann die Orig.-Liste verändern
                ClientCallbacks.Add(OperationContext.Current.GetCallbackChannel<ITimerCallback>());
            }
        }
    }
}
