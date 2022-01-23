using UnityEngine;
using UnityEngine.AI;
using System;
using Zenject;
using Game.Components.Events;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Movements.Contracts;
using Mirror;

namespace Game.Components.Navigations
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavigationBehaviour : NetworkBehaviour
    {
        public bool fullTurning = true;
        public Vector3 lastPosition;
        public bool isMoving = false;
        public bool hasPathPending = false;
        NavMeshAgent _navMeshAgent;
        INavigationRotation _navigationRotation;
        INavigationMovement _navigationMovement;
        INavigationPathPending _navigationPathPending;
        IMovementDetection _movementDetection;
        
        [Inject]
        public void Construct(
            INavigationRotation navigationRotation,
            INavigationMovement navigationMovement,
            INavigationPathPending navigationPathPending,
            IMovementDetection movementDetection
        ) {
            _navigationRotation = navigationRotation;
            _navigationMovement = navigationMovement;
            _navigationPathPending = navigationPathPending;
            _movementDetection = movementDetection;
        }

        [Client]
        public override void OnStartClient()
        {
            if(!hasAuthority) return;

            _navMeshAgent = this.transform.GetComponent<NavMeshAgent>();
            
            _navMeshAgent.Warp(transform.position);

            ConfigureNavMeshSettings();
        }

        [ClientCallback]
        void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        [Client]
        void ConfigureNavMeshSettings()
        {
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        [Client]
        void HandleMovement()
        {
            CheckMovement();

            transform.position = _navigationMovement.Movement(
                transform,
                _navMeshAgent,
                Time.fixedDeltaTime
            );
        }

        [Client]
        void CheckMovement() {
            Vector3 dist = transform.position - lastPosition;
            float currentSpeed = dist.magnitude / Time.deltaTime;
            lastPosition = transform.position;
            
            isMoving = currentSpeed > 1f;

            hasPathPending = _navigationPathPending.IsPathPending(_navMeshAgent);
        }

        [Client]
        void HandleRotation()
        {
            if(!isMoving) return;

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