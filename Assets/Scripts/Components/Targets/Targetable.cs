using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.Targets
{
    public class Targetable : MonoBehaviour
    {
        [SerializeField] private Transform aimAtPoint = null;

        public Transform GetAimAtPoint()
        {
            return aimAtPoint;
        }
    }
}