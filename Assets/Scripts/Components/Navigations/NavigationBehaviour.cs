using UnityEngine;
using UnityEngine.AI;
using Game.Components.Navigations.Contracts;
using Game.Components.Movements.Contracts;
using Game.Components.Movements;
using Mirror;
using UnityEngine.Events;

namespace Game.Components.Navigations
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavigationBehaviour : NetworkBehaviour
    {
        public bool fullTurning = true;
        public Vector3 lastPosition;
        [SyncVar] public bool isMoving = false;
        [SyncVar] public bool hasPathPending = false;
        NavMeshAgent _navMeshAgent;
        NavigationRotation _navigationRotation;
        INavigationMovement _navigationMovement;
        INavigationPathPending _navigationPathPending;
        IMovementDetection _movementDetection;

        public override void OnStartClient()
        {
            _navigationRotation = new NavigationRotation();
            _navigationMovement = new NavigationMovement();
            _navigationPathPending = new NavigationPathPending();
            _movementDetection = new MovementDetection();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _navMeshAgent.Warp(transform.position);

            ConfigureNavMeshSettings();
        }

        [Client]
        void ConfigureNavMeshSettings()
        {
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        [ClientCallback]
        void Update()
        // void FixedUpdate()
        {
            if (isClient && hasAuthority) {
                HandleMovement();
                HandleRotation();
            }
        }

        [ClientCallback]
        void HandleMovement()
        {
            CmdMovement(_navMeshAgent.nextPosition);
        }

        [Command]
        void CmdMovement(Vector3 nextPosition)
        {
            CheckMovement();
            RpcMovement(nextPosition);
        }

        void CheckMovement()
        {
            Vector3 dist = transform.position - lastPosition;
            float currentSpeed = dist.magnitude / Time.deltaTime;
            lastPosition = transform.position;

            isMoving = currentSpeed > 1f;

            hasPathPending = _navigationPathPending.IsPathPending(_navMeshAgent);
        }

        [ClientRpc]
        void RpcMovement(Vector3 nextPosition)
        {
            transform.position = _navigationMovement.Movement(
                transform,
                nextPosition,
                Time.fixedDeltaTime
            );
        }

        [ClientCallback]
        void HandleRotation()
        {
             if(!isMoving) return;

            CmdRotation(_navMeshAgent.nextPosition);
        }

        [Command]
        void CmdRotation(Vector3 nextPosition)
        {
            Quaternion rotation = _navigationRotation.Rotation(
                transform,
                nextPosition,
                Time.deltaTime,
                5f,
                fullTurning
            );

            RpcRotation(rotation);
        }

        [ClientRpc]
        void RpcRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        [Client]
        public void SetDestination(Vector3 destination)
        {
            // if(!NavMesh.SamplePosition(
            //     destination,
            //     out NavMeshHit hit,
            //     1f,
            //     NavMesh.AllAreas
            // )) return;
            _navMeshAgent.SetDestination(destination);
        }
    }
}