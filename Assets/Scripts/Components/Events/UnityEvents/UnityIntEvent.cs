using UnityEngine;
using UnityEngine.Events;
using Game.Components.Events;
using Game.Components.Events.Contracts;

namespace Game.Components.Events.UnityEvents
{
    [System.Serializable] public class UnityIntEvent : UnityEvent<int> {}
}