using System.ComponentModel;
using UnityEngine;
using Zenject;
using Game.Components.Buildings;
using Game.Components.Buildings.Contracts;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Selections;

namespace Game.Components.Buildings
{
    public class BuildingInstaller : Installer<BuildingInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IBuildingManager>()
                .To<BuildingManager>()
                .AsSingle()
            ;
        }
    }
}