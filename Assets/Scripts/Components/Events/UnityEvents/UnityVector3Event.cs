using UnityEngine;
using UnityEngine.Events;
using Game.Components.Events;
using Game.Components.Events.Contracts;

namespace Game.Components.Events.UnityEvents
{
    [System.Serializable] public class UnityVector3Event : UnityEvent<Vector3> {}
}