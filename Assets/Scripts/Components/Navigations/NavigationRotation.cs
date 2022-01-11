using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Rotations;
using Game.Components.Rotations.Contracts;

namespace Game.Components.Navigations
{
    public class NavigationRotation : Contracts.INavigationRotation
    {
        ISmoothTargetRotation _smoothTargetRotation;

        [Inject]
        public void Construct(
            ISmoothTargetRotation smoothTargetRotation
        ) {
            _smoothTargetRotation = smoothTargetRotation;
        }

        public Quaternion Rotation(
            Transform transform,
            NavMeshAgent navMeshAgent,
            float deltaTime,
            float rotationSpeed = 5f,
            bool fullTurn = true
        ) {
            if(fullTurn == false) {
                Vector3 direction = (navMeshAgent.nextPosition - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);

                if(Vector3.Angle(direction, transform.forward) >= 90) {
                    return _smoothTargetRotation.Rotation(
                        navMeshAgent.nextPosition,
                        transform.rotation,
                        transform.position,
                        deltaTime,
                        rotationSpeed
                    );
                }
            }

            return _smoothTargetRotation.Rotation(
                transform.position,
                transform.rotation,
                navMeshAgent.nextPosition,
                deltaTime,
                rotationSpeed
            );
        }
    }
}