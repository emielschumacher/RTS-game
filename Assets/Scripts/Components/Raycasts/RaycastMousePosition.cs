using UnityEngine;
using UnityEngine.Events;
using Game.Components.Raycasts;
using Game.Components.Raycasts.Contracts;
using Game.Components.Scenes.Contracts;
using Game.Components.Scenes;

namespace Game.Components.Raycasts
{
    public class RaycastMousePosition : IRaycastMousePosition
    {
        ISceneManager _sceneManager;

        public void Awake() {
            _sceneManager = new SceneManager();
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