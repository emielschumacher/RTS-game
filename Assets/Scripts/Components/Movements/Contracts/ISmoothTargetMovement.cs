using UnityEngine;

namespace Game.Components.Movements.Contracts
{  
    public interface ISmoothTargetMovement
    {
        Vector3 Movement(
            Vector3 currentPosition,
            Vector3 targetPosition,
            float deltaTime,
            float speed = 5f
        );
    }
}