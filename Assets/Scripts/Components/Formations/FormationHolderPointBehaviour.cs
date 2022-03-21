using UnityEngine;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationHolderPointBehaviour : NetworkBehaviour
    {
        public Vector3 formationOffset = Vector3.zero;
        public FormationHolderBehaviour formationHolder;


        private void Update()
        {
            transform.position = formationHolder.transform.position + formationOffset;
        }
    }
}