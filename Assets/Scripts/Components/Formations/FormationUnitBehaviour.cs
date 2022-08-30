using UnityEngine;
using System;
using Game.Components.Navigations;
using Game.Components.Selections.Selectables;
using Game.Components.Targets;
using Mirror;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    [RequireComponent(typeof(AbstractSelectable))]
    public class FormationUnitBehaviour : NetworkBehaviour
    {
        public static event Action<FormationUnitBehaviour> ServerOnFormationUnitSpawned;
        public static event Action<FormationUnitBehaviour> ServerOnFormationUnitDespawned;
        public static event Action<FormationUnitBehaviour> AuthorityOnFormationUnitSpawned;
        public static event Action<FormationUnitBehaviour> AuthorityOnFormationUnitDespawned;
        NavigationBehaviour _navigationBehaviour;
        
        public Vector3 localStartPosition;
        public FormationHolderBehaviour formationHolderBehaviour;
        public Vector3 formationOffset = Vector3.zero;

        void Start()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();
        }

        [Server]
        public override void OnStartServer()
        {
            ServerOnFormationUnitSpawned?.Invoke(this);
        }

        [Server]
        public override void OnStopServer()
        {
            ServerOnFormationUnitDespawned?.Invoke(this);
        }

        [Client]
        public override void OnStartClient()
        {
            if (!hasAuthority || !isClient) return;

            AuthorityOnFormationUnitSpawned?.Invoke(this);
        }

        [Client]
        public override void OnStopClient()
        {
            if (!hasAuthority || !isClient) return;

            AuthorityOnFormationUnitDespawned?.Invoke(this);
        }
        
        [ServerCallback]
        void Update()
        {
            _navigationBehaviour.SetDestination(
                formationHolderBehaviour.transform.rotation * localStartPosition + formationHolderBehaviour.transform.position
                //formationHolderBehaviour.transform.TransformPoint(localStartPosition)
            );
        }
    }
}