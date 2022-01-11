using UnityEngine;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using Game.Components.Events.Types;
using System.Collections;
using System.Collections.Generic;

namespace Game.Components.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "Game Events/Int Event")]
    public class IntEvent : BaseGameEvent<int>{}
}