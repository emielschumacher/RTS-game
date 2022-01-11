using UnityEngine;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using Game.Components.Events.Types;
using System.Collections;
using System.Collections.Generic;

namespace Game.Components.Events
{
    [CreateAssetMenu(fileName = "New Vector3 Event", menuName = "Game Events/Vector3 Event")]
    public class Vector3Event : BaseGameEvent<Vector3>{}
}