using System;
using System.Collections.Generic;
using UnityEngine;

namespace PushMechannic
{
    public interface IPusher
    {
        event Action<IPusher> OnPushingComplete;
        void Push(List<Rigidbody> rigidbodies);
    }
}