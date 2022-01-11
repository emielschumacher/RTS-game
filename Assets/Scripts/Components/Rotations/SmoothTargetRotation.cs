using UnityEngine;

namespace Game.Components.Rotations
{
    public class SmoothTargetRotation : Contracts.ISmoothTargetRotation
    {
        Quaternion _rotation = Quaternion.identity;
        Vector3 _lookRotation;

        public Quaternion Rotation (
            Vector3 currentPosition,
            Quaternion currentRotation,
            Vector3 targetPosition,
            float deltaTime,
            float rotationSpeed = 5f
        ) {
            _rotation = Quaternion.identity;
            _lookRotation = targetPosition - currentPosition;
            
            if((_lookRotation) == Vector3.zero) {
                return _rotation;
            }

            _rotation = Quaternion.LookRotation(_lookRotation);

            return Quaternion.Slerp(
                currentRotation,
                _rotation,
                rotationSpeed * deltaTime
            );
        }
    }
}