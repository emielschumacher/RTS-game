using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Zenject;
using Game.Components.Selections.Selectables;
using Mirror;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    [RequireComponent(typeof(AbstractSelectable))]
    public class FormationUnitBehaviour : NetworkBehaviour
    {
        [HideInInspector] public Transform formationHolderPoint;
        public static event Action<FormationUnitBehaviour> ServerOnFormationUnitSpawned;
        public static event Action<FormationUnitBehaviour> ServerOnFormationUnitDespawned;
        public static event Action<FormationUnitBehaviour> AuthorityOnFormationUnitSpawned;
        public static event Action<FormationUnitBehaviour> AuthorityOnFormationUnitDespawned;
        NavigationBehaviour _navigationBehaviour;

        #region Server

        public override void OnStartServer()
        {
            ServerOnFormationUnitSpawned?.Invoke(this);
        }

        public override void OnStopServer()
        {
            ServerOnFormationUnitDespawned?.Invoke(this);
        }

        #endregion

        #region Client

        [Client]
        public override void OnStartClient()
        {
            if(!isClientOnly || !hasAuthority) return;

            AuthorityOnFormationUnitSpawned?.Invoke(this);
        }

        [Client]
        public override void OnStopClient()
        {
            if(!isClientOnly || !hasAuthority) return;
            
            AuthorityOnFormationUnitDespawned?.Invoke(this);
        }

        [Client]
        void Awake()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();
        }

        [Client]
        void Update()
        {
            if(!formationHolderPoint) return; 

            _navigationBehaviour.SetDestination(formationHolderPoint.position);
        }

        public class Factory : PlaceholderFactory<FormationUnitBehaviour> { }

        #endregion
    }
}