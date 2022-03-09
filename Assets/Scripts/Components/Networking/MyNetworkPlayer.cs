using UnityEngine;
using Game.Components.Formations;
using System.Collections.Generic;
using Game.Components.Selections.Selectables;
using Game.Components.Selections.Contracts;
using Mirror;

namespace Game.Components.Networking
{
    public class MyNetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private List<FormationUnitBehaviour> _myFormationUnits = new List<FormationUnitBehaviour>();

        [Server]
        public override void OnStartServer()
        {
            FormationUnitBehaviour.ServerOnFormationUnitSpawned += ServerHandleUnitSpawned;
            FormationUnitBehaviour.ServerOnFormationUnitDespawned += ServerHandleUnitDespawned;
        }

        [Server]
        public override void OnStopServer()
        {
            FormationUnitBehaviour.ServerOnFormationUnitSpawned -= ServerHandleUnitSpawned;
            FormationUnitBehaviour.ServerOnFormationUnitDespawned -= ServerHandleUnitDespawned;
        }

        [Server]
        private void ServerHandleUnitSpawned(FormationUnitBehaviour unit)
        {
            if(unit.connectionToClient.connectionId != connectionToClient.connectionId) return;

            _myFormationUnits.Add(unit);
        }

        [Server]
        private void ServerHandleUnitDespawned(FormationUnitBehaviour unit)
        {
            if(unit.connectionToClient.connectionId != connectionToClient.connectionId) return;

            _myFormationUnits.Remove(unit);
        }

        [Client]
        public override void OnStartClient()
        {
            FormationUnitBehaviour.AuthorityOnFormationUnitSpawned += ServerHandleUnitSpawned;
            FormationUnitBehaviour.AuthorityOnFormationUnitDespawned += ServerHandleUnitDespawned;
        }

        [Client]
        public override void OnStopClient()
        {
            FormationUnitBehaviour.AuthorityOnFormationUnitSpawned -= ServerHandleUnitSpawned;
            FormationUnitBehaviour.AuthorityOnFormationUnitDespawned -= ServerHandleUnitDespawned;
        }

        [Client]
        private void AuthorityHandleUnitSpawned(FormationUnitBehaviour unit)
        {
            if(!hasAuthority) return;

            _myFormationUnits.Add(unit);
        }

        [Client]
        private void AuthorityHandleUnitDespawned(FormationUnitBehaviour unit)
        {
            if(!hasAuthority) return;

            _myFormationUnits.Remove(unit);
        }

        [Client]
        public List<FormationUnitBehaviour> GetMyFormationUnits()
        {
            return _myFormationUnits;
        }
    }
}