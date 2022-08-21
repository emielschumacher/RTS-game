using UnityEngine;
using UnityEngine.AI;
using Game.Components.Navigations.Contracts;
using Mirror;

namespace Game.Components.Navigations
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavigationBehaviour : NetworkBehaviour
    {
        public bool fullTurning = true;
        [SyncVar] public Vector3 lastPosition;
        [SyncVar] public bool isMoving = false;
        [SyncVar] public bool hasPathPending = false;
        NavMeshAgent _navMeshAgent;
        NavigationRotation _navigationRotation;
        INavigationMovement _navigationMovement;
        INavigationPathPending _navigationPathPending;

        public void Awake()
        {
            _navigationRotation = new NavigationRotation();
            _navigationMovement = new NavigationMovement();
            _navigationPathPending = new NavigationPathPending();
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
        void FixedUpdate()
        {
            if (!hasAuthority || !isClient) return;

            CmdMovement();
            CmdRotation();
        }

        [Command]
        void CmdRotation()
        {
            if(!isMoving) return;

            transform.rotation = _navigationRotation.Rotation(
                transform,
                _navMeshAgent.nextPosition,
                Time.fixedDeltaTime,
                5f,
                fullTurning
            );
        }

        [Command]
        void CmdMovement()
        {
            CheckMovement();

            transform.position = _navigationMovement.Movement(
                transform,
                _navMeshAgent.nextPosition,
                Time.fixedDeltaTime
            );
        }

        void CheckMovement()
        {
            Vector3 dist = transform.position - lastPosition;
            float currentSpeed = dist.magnitude / Time.fixedDeltaTime;
            lastPosition = transform.position;

            isMoving = currentSpeed > 1f;

            hasPathPending = _navigationPathPending.IsPathPending(_navMeshAgent);
        }

        [Client]
        public void SetDestination(
            Vector3 destination
        ) {
            if (!hasAuthority || !isClient) return;

            CmdSetDestination(destination);
        }

        [Command]
        public void CmdSetDestination(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
        }
    }
}