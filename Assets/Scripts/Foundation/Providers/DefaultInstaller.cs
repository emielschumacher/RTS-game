using UnityEngine;
using Zenject;
using System;
using Game.Components.Formations;
using Game.Components.Formations.Contracts;
using Game.Components.Spawners;
using Game.Components.Networking;

namespace Game.Foundation.Providers {
    public class DefaultInstaller : MonoInstaller
    {
        public MyNetworkPlayer myNetworkPlayer;
        public GameObject spawnerPrefab;
        public GameObject formationPrefab;
        public GameObject formationHolderPointPrefab;
        public GameObject formationUnitPrefab;

        public override void InstallBindings()
        {
            Container
                .BindFactory<MyNetworkPlayer, MyNetworkPlayer.Factory>()
                .FromComponentInNewPrefab(myNetworkPlayer)
            ;
            Container
                .BindFactory<FormationUnitBehaviour, FormationUnitBehaviour.Factory>()
                .FromComponentInNewPrefab(formationUnitPrefab)
            ;
            Container
                .BindFactory<FormationHolderPointBehaviour, FormationHolderPointBehaviour.Factory>()
                .FromComponentInNewPrefab(formationHolderPointPrefab)
            ;
            Container
                .BindFactory<SpawnerBehaviour, SpawnerBehaviour.Factory>()
                .FromComponentInNewPrefab(spawnerPrefab)
            ;
            Container
                .BindFactory<FormationBehaviour, FormationBehaviour.Factory>()
                .FromComponentInNewPrefab(formationPrefab)
            ;

            Game.Components.ComponentsInstaller.Install(Container);
        }
    }
}