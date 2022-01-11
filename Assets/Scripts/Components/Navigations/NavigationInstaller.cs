using UnityEngine;
using Zenject;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;

namespace Game.Components.Navigations
{
    public class NavigationInstaller : Installer<NavigationInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<INavigationMovement>()
                .To<NavigationMovement>()
                .AsSingle();
            Container
                .Bind<INavigationRotation>()
                .To<NavigationRotation>()
                .AsSingle();
            Container
                .Bind<INavigationPathPending>()
                .To<NavigationPathPending>()
                .AsSingle();
        }
    }
}