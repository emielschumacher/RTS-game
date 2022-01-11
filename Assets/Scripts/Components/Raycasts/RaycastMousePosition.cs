using UnityEngine;
using UnityEngine.Events;
using Game.Components.Raycasts;
using Game.Components.Raycasts.Contracts;
using Game.Components.Scenes.Contracts;
using Zenject;

namespace Game.Components.Raycasts
{
    public class RaycastMousePosition : IRaycastMousePosition
    {
        ISceneManager _sceneManager;

        [Inject]
        public void Construct(
            ISceneManager sceneManager
        ) {
            _sceneManager = sceneManager;
        }

        public RaycastHit GetRaycastHit()
        {
            Camera camera = _sceneManager.GetCamera();
            RaycastHit hit;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit, Mathf.Infinity);
            
            return hit;
        }
    }
}