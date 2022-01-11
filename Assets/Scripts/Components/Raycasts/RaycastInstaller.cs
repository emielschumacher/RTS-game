using Zenject;
using Game.Components.Raycasts;
using Game.Components.Raycasts.Contracts;

namespace Game.Components.Raycasts
{
    public class RaycastInstaller : Installer<RaycastInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Contracts.IRaycastMousePosition>()
                .To<RaycastMousePosition>()
                .AsSingle();
        }
    }
}