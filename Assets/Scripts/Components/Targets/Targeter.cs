using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using Game.Components.Navigations;

namespace Game.Components.Targets
{
    [RequireComponent(typeof(NavigationBehaviour))]
    public class Targeter : NetworkBehaviour
    {
        [SerializeField] private Targetable _target;
        private NavigationBehaviour _navigationBehaviour;
        public UnityEvent onTargetSetEvent;
        public UnityEvent onTargetClearEvent;

        public float chaseRange = 1f;

        void Start()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();
        }

        public Targetable GetTarget()
        {
            return _target;
        }

        [ServerCallback]
        void Update()
        {
            if(!_target) {
                return;
            }

            if(
                (_target.transform.position - transform.position).sqrMagnitude
                > chaseRange * chaseRange
            ) {
                ChaseTarget();
            }
        }

        [Server]
        void ChaseTarget()
        {
            _navigationBehaviour.SetDestination(_target.transform.position);
        }


        [Command]
        public void CmdSetTarget(GameObject targetGameObject)
        {

            if (!targetGameObject.TryGetComponent<Targetable>(
                out Targetable newTarget
            )) {
                return;
            }

            SetTarget(newTarget);
            RpcSetTarget(newTarget);
        }

        [ClientRpc]
        void RpcSetTarget(Targetable newTarget)
        {
            SetTarget(newTarget);
        }

        void SetTarget(Targetable newTarget)
        {
            onTargetSetEvent.Invoke();
            _target = newTarget;
        }

        void ClearTarget()
        {
            onTargetClearEvent.Invoke();
            _target = null;
        }

        [Command]
        public void CmdClearTarget()
        {
            ClearTarget();
            RpcClearTarget();
            
        }

        [ClientRpc]
        public void RpcClearTarget()
        {
            ClearTarget();
        }
    }
}