using UnityEngine;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using System.Collections;
using System.Collections.Generic;
using Game.Components.Events.UnityEvents;
using Game.Components.Events.Types;

namespace Game.Components.Events.Listeners
{
    public class VoidListener : BaseGameEventListener<Void, VoidEvent, UnityVoidEvent> {}
}