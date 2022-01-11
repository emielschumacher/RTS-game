using UnityEngine;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using System.Collections;
using System.Collections.Generic;
using Game.Components.Events.UnityEvents;

namespace Game.Components.Events.Listeners
{
    public class IntListener : BaseGameEventListener<int, IntEvent, UnityIntEvent> {}
}