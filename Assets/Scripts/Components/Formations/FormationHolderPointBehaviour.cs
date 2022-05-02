using UnityEngine;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationHolderPointBehaviour : NetworkBehaviour
    {
        public Vector3 formationOffset = Vector3.zero;
        public FormationHolderBehaviour formationHolder;

         private Vector3 position, forward, up;

        [ClientCallback]
        void Update()
        {
            //if (!isClient || !hasAuthority) return;

            if (!formationHolder) return;

            CmdMove();
        }

        void Start()
        {
            //position = formationHolder.transform.InverseTransformPoint(transform.position.normalized);
            //forward = formationHolder.transform.InverseTransformDirection(transform.forward);
            //up = formationHolder.transform.InverseTransformDirection(transform.up);
        }

        [Command]
        void CmdMove()
        {
            //transform.position = formationHolder.transform.position + formationOffset;

            //var newpos = formationHolder.transform.TransformPoint(position);
            //var newfw = formationHolder.transform.TransformDirection(forward);
            //var newup = formationHolder.transform.TransformDirection(up);
            //var newrot = Quaternion.LookRotation(newfw, newup);

            //transform.position = newpos.normalized;
            //transform.rotation = newrot;

            transform.position = formationHolder.transform.rotation * formationOffset + formationHolder.transform.position;
            //transform.rotation = formationHolder.transform.rotation;
        }
    }
}