using UnityEngine;
using Zenject;
using System;
using Game.Components.Formations;
using Game.Components.Formations.Contracts;
using Game.Foundation.Providers;

namespace Game.Components.Formations
{
    public class FormationInstaller : Installer<FormationInstaller>
    {
        // [Inject]
        // Settings _settings = null;       

        public override void InstallBindings()
        {
            Container
                .Bind<IFormation>()
                .To<Formation>()
                .AsSingle()
            ;
        }

        // [Serializable]
        // public class Settings
        // {
        //     public GameObject formationUnitPrefab;
        // }
    }
}