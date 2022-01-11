using UnityEngine;
using Zenject;
using Game.Components.Scenes;
using Game.Components.Scenes.Contracts;

namespace Game.Components.Scenes
{
    public class SceneInstaller : Installer<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Contracts.ISceneManager>()
                .To<SceneManager>()
                .AsSingle();
        }
    }
}