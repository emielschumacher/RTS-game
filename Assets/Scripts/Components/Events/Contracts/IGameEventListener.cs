using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Components.Events.Contracts
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}