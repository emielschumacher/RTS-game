using UnityEngine;

namespace Game.Components.Raycasts.Contracts
{
    public interface IRaycastMousePosition
    {
        RaycastHit GetRaycastHit();
    }
}