using UnityEngine;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using Game.Components.Events.Types;
using System.Collections;
using System.Collections.Generic;

namespace Game.Components.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}