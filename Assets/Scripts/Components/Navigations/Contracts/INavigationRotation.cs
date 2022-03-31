using UnityEngine;

namespace Game.Components.Navigations.Contracts
{  
    public interface INavigationRotation
    {
        Quaternion Rotation(
            Transform transform,
            Vector3 nextPosition,
            float deltaTime,
            float rotationSpeed = 5f,
            bool fullTurn = true
        );
    }
}