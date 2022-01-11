using UnityEngine;

namespace Game.Components.Rotations.Contracts
{  
    public interface ISmoothTargetRotation
    {
        public Quaternion Rotation(
            Vector3 currentPosition,
            Quaternion currentRotation,
            Vector3 targetPosition,
            float deltaTime,
            float rotationSpeed = 5f
        );
    }
}