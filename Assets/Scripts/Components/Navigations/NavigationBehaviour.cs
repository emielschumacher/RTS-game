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
        [SyncVar] Vector3 syncPosition;
        public bool fullTurning = true;
        public Vector3 lastPosition;
        public bool isMoving = false;
        public bool hasPathPending = false;
        NavMeshAgent _navMeshAgent;
        NavigationRotation _navigationRotation;
        INavigationMovement _navigationMovement;
        INavigationPathPending _navigationPathPending;
        IMovementDetection _movementDetection;

        public override void OnStartClient()
        {
            _navigationRotation = new NavigationRotation();
            _navigationPathPending = new NavigationPathPending();
            _movementDetection = new MovementDetection();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _navMeshAgent.Warp(transform.position);

            NavigationManager
                .instance.navigationMarkerBehaviour
                .onNewMarkerPositionEvent += HandleNewMarkerPositionEvent;

            ConfigureNavMeshSettings();
        }

        private void HandleNewMarkerPositionEvent(
            Vector3 position
        )
        {
            SetDestination(position);
        }

        void Update()
        // void FixedUpdate()
        {
            if (isClient && hasAuthority) {
                HandleMovement();
                HandleRotation();
            }
        }

        [Client]
        void ConfigureNavMeshSettings()
        {
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        [ClientCallback]
        void HandleMovement()
        {
            // CheckMovement();
            CmdMovement(_navMeshAgent.nextPosition);
        }

        [Command]
        void CmdMovement(Vector3 nextPosition) {
            RpcMovement(nextPosition);
        }

        [ClientRpc]
        void RpcMovement(Vector3 nextPosition)
        {
            transform.position = nextPosition;
            // transform.position = _navigationMovement.Movement(
            //     transform,
            //     _navMeshAgent,
            //     Time.fixedDeltaTime
            // );
        }

        // [ClientCallback]
        // void CheckMovement() {
        //     Vector3 dist = transform.position - lastPosition;
        //     float currentSpeed = dist.magnitude / Time.deltaTime;
        //     lastPosition = transform.position;
            
        //     isMoving = currentSpeed > 1f;

        //     hasPathPending = _navigationPathPending.IsPathPending(_navMeshAgent);
        // }

        [ClientCallback]
        private void HandleRotation()
        {
            // if(!isMoving) return;

            transform.rotation = _navigationRotation.Rotation(
                transform,
                _navMeshAgent,
                Time.deltaTime,
                5f,
                fullTurning
            );
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