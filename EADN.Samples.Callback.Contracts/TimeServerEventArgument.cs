using System;
using System.Runtime.Serialization;

namespace EADN.Samples.Callback.Contracts
{
    [DataContract(Name = "TimeServerEventArgument", Namespace = "EADN.Samples.Callback.Contracts")]
    public class TimeServerEventArgument : EventArgs
    {
        [DataMember(Name = "CurrentTime")]
        public DateTime CurrentTime { get; private set; }

        public TimeServerEventArgument(DateTime currentTime)
        {
            CurrentTime = currentTime;
        }
    }
}