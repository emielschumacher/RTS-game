using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class ComponentsInstaller : Installer<ComponentsInstaller>
    {
        public override void InstallBindings()
        {
            Game.Components.Scenes.SceneInstaller.Install(Container);
            Game.Components.Selections.SelectionInstaller.Install(Container);
            Game.Components.Movements.MovementInstaller.Install(Container);
            Game.Components.Navigations.NavigationInstaller.Install(Container);
            Game.Components.Rotations.RotationInstaller.Install(Container);
            Game.Components.Raycasts.RaycastInstaller.Install(Container);
            Game.Components.Buildings.BuildingInstaller.Install(Container);
            Game.Components.Formations.FormationInstaller.Install(Container);
        }
    }
}