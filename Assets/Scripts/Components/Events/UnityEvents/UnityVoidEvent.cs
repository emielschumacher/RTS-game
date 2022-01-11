using UnityEngine;
using UnityEngine.Events;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using Game.Components.Events.Types;

namespace Game.Components.Events.UnityEvents
{
    [System.Serializable] public class UnityVoidEvent : UnityEvent<Void> {}
}