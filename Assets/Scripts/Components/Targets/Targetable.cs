using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Game.Components.Targets
{
    public class Targetable : NetworkBehaviour
    {
        [SerializeField] private Transform aimAtPoint = null;

        public Transform GetAimAtPoint()
        {
            return aimAtPoint;
        }
    }
}