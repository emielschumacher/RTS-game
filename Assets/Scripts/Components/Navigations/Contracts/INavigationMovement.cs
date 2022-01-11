using UnityEngine;
using UnityEngine.AI;

namespace Game.Components.Navigations.Contracts
{  
    public interface INavigationMovement
    {
        Vector3 Movement(
            Transform transform,
            NavMeshAgent navMeshAgent,
            float deltaTime
        );
    }
}