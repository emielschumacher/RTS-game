using UnityEngine;
using Game.Components.Formations;
using System.Collections.Generic;
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
            if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;

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
            if (!isClientOnly) return;

            FormationUnitBehaviour.AuthorityOnFormationUnitSpawned += AuthorityHandleUnitSpawned;
            FormationUnitBehaviour.AuthorityOnFormationUnitDespawned += AuthorityHandleUnitDespawned;
        }

        [Client]
        public override void OnStopClient()
        {
            if (!isClientOnly) return;

            FormationUnitBehaviour.AuthorityOnFormationUnitSpawned -= AuthorityHandleUnitSpawned;
            FormationUnitBehaviour.AuthorityOnFormationUnitDespawned -= AuthorityHandleUnitDespawned;
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