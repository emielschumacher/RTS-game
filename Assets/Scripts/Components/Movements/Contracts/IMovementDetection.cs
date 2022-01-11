using UnityEngine;

namespace Game.Components.Movements.Contracts
{  
    public interface IMovementDetection
    {
        bool IsMoving(
            Vector3 currentPosition
        );
    }
}