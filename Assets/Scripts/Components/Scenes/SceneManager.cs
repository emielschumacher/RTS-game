using UnityEngine;
using Game.Components.Scenes.Contracts;

namespace Game.Components.Scenes
{
    public class SceneManager : ISceneManager
    {
        Camera _camera;

        public Camera GetCamera()
        {
            if(!_camera) {
                _camera = Camera.main;
            }

            return _camera;
        }
    }
}