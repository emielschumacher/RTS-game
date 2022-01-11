using UnityEngine;
using Game.Components.Formations;
using System.Collections.Generic;
using Game.Components.Selections.Selectables;
using Game.Components.Selections.Contracts;
using Mirror;
using Zenject;

namespace Game.Components.Networking
{
    public class MyNetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private List<FormationUnitBehaviour> _myFormationUnits = new List<FormationUnitBehaviour>();

        public List<FormationUnitBehaviour> GetMyFormationUnits()
        {
            return _myFormationUnits;
        }

        #region Server
        public override void OnStartServer()
        {
            FormationUnitBehaviour.ServerOnFormationUnitSpawned += ServerHandleUnitSpawned;
            FormationUnitBehaviour.ServerOnFormationUnitDespawned += ServerHandleUnitDespawned;
        }

        public override void OnStopServer()
        {
            FormationUnitBehaviour.ServerOnFormationUnitSpawned -= ServerHandleUnitSpawned;
            FormationUnitBehaviour.ServerOnFormationUnitDespawned -= ServerHandleUnitDespawned;
        }

        private void ServerHandleUnitSpawned(FormationUnitBehaviour unit)
        {
            if(unit.connectionToClient.connectionId != connectionToClient.connectionId) return;

            _myFormationUnits.Add(unit);
        }

        private void ServerHandleUnitDespawned(FormationUnitBehaviour unit)
        {
            if(unit.connectionToClient.connectionId != connectionToClient.connectionId) return;

            _myFormationUnits.Remove(unit);
        }

        #endregion

        #region Client

        public override void OnStartClient()
        {
            if(!isClientOnly) return;

            FormationUnitBehaviour.AuthorityOnFormationUnitSpawned += ServerHandleUnitSpawned;
            FormationUnitBehaviour.AuthorityOnFormationUnitDespawned += ServerHandleUnitDespawned;
        }

        public override void OnStopClient()
        {
            if(!isClientOnly) return;

            FormationUnitBehaviour.AuthorityOnFormationUnitSpawned -= ServerHandleUnitSpawned;
            FormationUnitBehaviour.AuthorityOnFormationUnitDespawned -= ServerHandleUnitDespawned;
        }

        private void AuthorityHandleUnitSpawned(FormationUnitBehaviour unit)
        {
            if(!hasAuthority) return;

            _myFormationUnits.Add(unit);
        }

        private void AuthorityHandleUnitDespawned(FormationUnitBehaviour unit)
        {
            if(!hasAuthority) return;

            _myFormationUnits.Remove(unit);
        }

        #endregion

        public class Factory : PlaceholderFactory<MyNetworkPlayer> { }
    }
}