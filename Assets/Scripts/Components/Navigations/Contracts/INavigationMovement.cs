using UnityEngine;
using UnityEngine.AI;

namespace Game.Components.Navigations.Contracts
{  
    public interface INavigationMovement
    {
        Vector3 Movement(
            Transform transform,
            Vector3 nextPosition,
            float deltaTime
        );
    }
}