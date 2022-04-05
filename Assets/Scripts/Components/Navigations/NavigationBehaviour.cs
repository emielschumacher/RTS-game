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

        public void Awake()
        {
            _navigationRotation = new NavigationRotation();
            _navigationMovement = new NavigationMovement();
            _navigationPathPending = new NavigationPathPending();
            _movementDetection = new MovementDetection();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _navMeshAgent.Warp(transform.position);

            ConfigureNavMeshSettings();
        }

        void ConfigureNavMeshSettings()
        {
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        [ClientCallback]
        void Update()
        {
            if (isClient && hasAuthority) {
                CmdMovement();
                CmdRotation();
            }
        }

        [Command]
        void CmdMovement()
        {
            CheckMovement();

            transform.position = _navigationMovement.Movement(
                transform,
                _navMeshAgent.nextPosition,
                Time.deltaTime
            );
        }

        void CheckMovement()
        {
            Vector3 dist = transform.position - lastPosition;
            float currentSpeed = dist.magnitude / Time.deltaTime;
            lastPosition = transform.position;

            isMoving = currentSpeed > 1f;

            hasPathPending = _navigationPathPending.IsPathPending(_navMeshAgent);
        }

        [Command]
        void CmdRotation()
        {
            if(!isMoving) return;

            Quaternion rotation = _navigationRotation.Rotation(
                transform,
                _navMeshAgent.nextPosition,
                Time.deltaTime,
                5f,
                fullTurning
            );
            
            transform.rotation = rotation;
        }

        [Client]
        public void SetDestination(
            Vector3 destination
        ) {
            if (!isClient && !hasAuthority) {
                Debug.Log("No isClient && no hasAuthority");

                return;
            }

            CmdSetDestination(destination);
        }

        [Command]
        public void CmdSetDestination(Vector3 destination)
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