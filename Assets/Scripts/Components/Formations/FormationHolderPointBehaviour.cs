using UnityEngine;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationHolderPointBehaviour : NetworkBehaviour
    {
        [HideInInspector] public Vector3 formationOffset = Vector3.zero;
        [HideInInspector] public FormationHolderBehaviour formationHolder;

        private void Update()
        {
            if (!formationHolder) return;

            transform.position = formationHolder.transform.position + formationOffset;
        }
    }
}