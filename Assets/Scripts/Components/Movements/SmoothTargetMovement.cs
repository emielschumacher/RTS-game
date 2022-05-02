using UnityEngine;

namespace Game.Components.Movements
{
    public class SmoothTargetMovement : Contracts.ISmoothTargetMovement
    {
        public Vector3 Movement(
            Vector3 currentPosition,
            Vector3 targetPosition,
            float deltaTime,
            float speed = 5f
        ) {
            return Vector3.Lerp(
                currentPosition,
                targetPosition,
                3f * deltaTime
            );
        }
    }
}