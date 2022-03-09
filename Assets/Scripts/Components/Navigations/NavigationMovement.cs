using UnityEngine;
using UnityEngine.AI;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Movements;
using Game.Components.Movements.Contracts;

namespace Game.Components.Navigations
{
    public class NavigationMovement : Contracts.INavigationMovement
    {
        ISmoothTargetMovement _smoothTargetMovement;

        public Vector3 Movement(
            Transform transform,
            NavMeshAgent navMeshAgent,
            float deltaTime
        ) {
            _smoothTargetMovement = new SmoothTargetMovement();
            
            return _smoothTargetMovement.Movement(
                transform.position,
                navMeshAgent.nextPosition,
                deltaTime
            );
        }
    }
}