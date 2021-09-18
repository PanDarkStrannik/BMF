using System.Collections.Generic;
using UnityEngine;

namespace PushMechannic
{ 
    public interface IPushable
    {
        bool TryPush(IPusher pusher);
    }
}