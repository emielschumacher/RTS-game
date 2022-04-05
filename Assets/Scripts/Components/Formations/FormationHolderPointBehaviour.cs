using UnityEngine;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationHolderPointBehaviour : NetworkBehaviour
    {
        [HideInInspector] public Vector3 formationOffset = Vector3.zero;
        public FormationHolderBehaviour formationHolder;
        
        [ClientCallback]
        void Update()
        {
            //if (!isClient || !hasAuthority) return;

            if (!formationHolder) return;

            CmdMove();
        }

        
        [Command]
        void CmdMove()
        {
            transform.position = formationHolder.transform.position + formationOffset;
        }
    }
}