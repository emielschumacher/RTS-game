using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Movements;
using Game.Components.Movements.Contracts;

namespace Game.Components.Navigations
{
    public class NavigationMovement : Contracts.INavigationMovement
    {
        ISmoothTargetMovement _smoothTargetMovement;

        [Inject]
        public void Construct(
            ISmoothTargetMovement smoothTargetMovement
        ) {
            _smoothTargetMovement = smoothTargetMovement;
        }

        public Vector3 Movement(
            Transform transform,
            NavMeshAgent navMeshAgent,
            float deltaTime
        ) {
            return _smoothTargetMovement.Movement(
                transform.position,
                navMeshAgent.nextPosition,
                deltaTime
            );
        }
    }
}