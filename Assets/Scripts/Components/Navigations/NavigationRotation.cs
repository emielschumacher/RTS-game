using UnityEngine;
using UnityEngine.AI;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Rotations;
using Game.Components.Rotations.Contracts;

namespace Game.Components.Navigations
{
    public class NavigationRotation : Contracts.INavigationRotation
    {
        ISmoothTargetRotation _smoothTargetRotation;
        Vector3 _direction;

        public Quaternion Rotation(
            Transform transform,
            Vector3 nextPosition,
            float deltaTime,
            float rotationSpeed = 5f,
            bool fullTurn = true
        ) {
            if (_smoothTargetRotation == null) {
                _smoothTargetRotation = new SmoothTargetRotation();
            }

            if(fullTurn == false) {
                _direction = (nextPosition - transform.position).normalized;

                if(_direction == Vector3.zero) return transform.rotation;

                if(Vector3.Angle(_direction, transform.forward) >= 90) {
                    return _smoothTargetRotation.Rotation(
                        nextPosition,
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
                nextPosition,
                deltaTime,
                rotationSpeed
            );
        }
    }
}