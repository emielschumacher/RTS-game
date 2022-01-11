using UnityEngine;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using System.Collections;
using System.Collections.Generic;
using Game.Components.Events.Listeners;

namespace Game.Components.Events
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for (int i = eventListeners.Count - 1; i >= 0 ; i--)
            {
                eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if(!eventListeners.Contains(listener)) {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if(eventListeners.Contains(listener)) {
                eventListeners.Remove(listener); 
            }
        }
    }
}