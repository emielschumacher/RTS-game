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

        [ServerCallback]
        //void FixedUpdate() // this is vissualy better
        void Update()
        {
            Movement();
            Rotation();
        }

        [Server]
        void Rotation()
        {
            if(!isMoving) return;

            transform.rotation = _navigationRotation.Rotation(
                transform,
                _navMeshAgent.nextPosition,
                Time.deltaTime,
                5f,
                fullTurning
            );
        }

        [Server]
        void Movement()
        {
            CheckMovement();

            transform.position = _navigationMovement.Movement(
                transform,
                _navMeshAgent.nextPosition,
                Time.deltaTime
            );
        }

        [Server]
        void CheckMovement()
        {
            Vector3 dist = transform.position - lastPosition;
            float currentSpeed = dist.magnitude / Time.deltaTime;
            lastPosition = transform.position;

            isMoving = currentSpeed > 1f;

            hasPathPending = _navigationPathPending.IsPathPending(_navMeshAgent);
        }
        
        [Server]
        public void SetDestination(
            Vector3 destination
        ) {
            _navMeshAgent.SetDestination(destination);
        }

        [Command]
        public void CmdSetDestination(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
        }
    }
}