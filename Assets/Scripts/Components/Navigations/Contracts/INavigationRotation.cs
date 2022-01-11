using UnityEngine;
using UnityEngine.AI;

namespace Game.Components.Navigations.Contracts
{  
    public interface INavigationRotation
    {
        Quaternion Rotation(
            Transform transform,
            NavMeshAgent navMeshAgent,
            float deltaTime,
            float rotationSpeed = 5f,
            bool fullTurn = true
        );
    }
}