using UnityEngine;
using System;
using Game.Components.Navigations;
using Game.Components.Selections.Selectables;
using Mirror;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    [RequireComponent(typeof(AbstractSelectable))]
    public class FormationUnitBehaviour : NetworkBehaviour
    {
        public Transform formationHolderPoint;
        public static event Action<FormationUnitBehaviour> ServerOnFormationUnitSpawned;
        public static event Action<FormationUnitBehaviour> ServerOnFormationUnitDespawned;
        public static event Action<FormationUnitBehaviour> AuthorityOnFormationUnitSpawned;
        public static event Action<FormationUnitBehaviour> AuthorityOnFormationUnitDespawned;
        NavigationBehaviour _navigationBehaviour;

        public override void OnStartServer()
        {
            ServerOnFormationUnitSpawned?.Invoke(this);
        }

        public override void OnStopServer()
        {
            ServerOnFormationUnitDespawned?.Invoke(this);
        }

        [Client]
        public override void OnStartClient()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();

            AuthorityOnFormationUnitSpawned?.Invoke(this);
        }

        [Client]
        public override void OnStopClient()
        {
            AuthorityOnFormationUnitDespawned?.Invoke(this);
        }

        [ClientCallback]
        void Update()
        {
            // if(!connectionToClient.isReady) return;
            if(!hasAuthority) return;
            if(!formationHolderPoint) return;

            _navigationBehaviour.SetDestination(formationHolderPoint.position);
        }
    }
}