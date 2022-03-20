using UnityEngine;
using Game.Components.Raycasts.Contracts;

namespace Game.Components.Raycasts
{
    public class RaycastMousePosition : IRaycastMousePosition
    {
        Camera _camera;

        public RaycastHit GetRaycastHit()
        {
            Camera camera = GetCamera();
            RaycastHit hit;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit, Mathf.Infinity);
            
            return hit;
        }

        public Camera GetCamera()
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }

            return _camera;
        }
    }
}