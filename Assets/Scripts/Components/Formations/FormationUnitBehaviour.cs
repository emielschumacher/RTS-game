using UnityEngine;
using System;
using Game.Components.Navigations;
using Game.Components.Selections.Selectables;
using Mirror;
using Game.Components.Formations.States.Contracts;
using Game.Components.Formations.States;
using Game.Components.Targets;

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
        public FormationHolderBehaviour formationHolderBehaviour;

        public Vector3 localStartPosition;
        public Vector3 formationOffset = Vector3.zero;

        [SerializeField]
        private IUnitState currentState;
        private NavigationBehaviour _navigationBehaviour;

        public HoldFormationPositionState holdFormationPositionState = new HoldFormationPositionState();
        public AttackState attackState = new AttackState();

        void Start()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();
            currentState = holdFormationPositionState;

            formationHolderBehaviour.GetTargeter().onTargetSetEvent.AddListener(newTarget => HandleOnTargetSetEvent(newTarget));
        }

        public void HandleOnTargetSetEvent(Targetable newTarget)
        {
            currentState = attackState;
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
            currentState = currentState.DoState(this);
        }

        public NavigationBehaviour GetNavigationBehaviour()
        {
            return _navigationBehaviour;
        }
    }
}