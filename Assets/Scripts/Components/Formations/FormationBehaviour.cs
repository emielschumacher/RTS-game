using System.Collections.Generic;
using UnityEngine;
using Game.Components.Formations;
using Game.Components.Formations.Contracts;
using Game.Components.Selections.Selectables.Contracts;
using Game.Components.Selections.Selectables;
using Zenject;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationBehaviour : NetworkBehaviour
    {
        public List<GameObject> unitList = new List<GameObject>();
        public FormationHolderBehaviour formationHolderBehaviour;
        public GameObject formationHolderPointPrefab;
        public int unitAmount;
        public SelectableGroup selectableGroup;
        IFormation _formation;
        [Inject] FormationUnitBehaviour.Factory _formationUnitFactory;
        [Inject] FormationHolderBehaviour.Factory _formationHolderFactory;
        [Inject] FormationHolderPointBehaviour.Factory _formationHolderPointFactory;

        [Inject]
        public void Construct(
            IFormation formation
        ) {
            _formation = formation;
        }

        [Client]
        public override void OnStartClient()
        {
            base.OnStartClient();

            selectableGroup = this.GetComponent<SelectableGroup>();
            // formationHolderBehaviour.formationBehaviour = this.GetComponent<FormationBehaviour>();
            CmdSpawnFormationUnits();
        }

        [Command]
        void CmdSpawnFormationUnits()
        {
            RpcSpawnFormationUnits();
        }

        [ClientRpc]
        private void RpcSpawnFormationUnits()
        {
            CreateFormationHolder();

            for (int unitNumber = 0; unitNumber < unitAmount; unitNumber++)
            {
                CreateFormationUnit(
                    CreateFormationHolderPoint(unitNumber)
                );
            }
        } 

        void CreateFormationHolder()
        {
            FormationHolderBehaviour formationHolder = _formationHolderFactory.Create();

            NetworkServer.Spawn(formationHolder.transform.gameObject, connectionToClient);

            formationHolderBehaviour = formationHolder;
            formationHolder.formationBehaviour = this;
            formationHolder.transform.position = transform.position;
            formationHolder.transform.rotation = transform.rotation;
        }

        void CreateFormationUnit(Transform formationHolderPoint)
        {
            FormationUnitBehaviour formationUnit = _formationUnitFactory.Create();

            NetworkServer.Spawn(formationUnit.transform.gameObject, connectionToClient);

            formationUnit.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);

            SelectableFormationUnit formationUnitSelectable = formationUnit.GetComponent<SelectableFormationUnit>();

            // Change this so this will also set for clients instead of server only!
            // Maybe add NetworkBehaviour?
            // NOTE: To execute code on the server, you must use commands!!! that is it.
            formationUnitSelectable.SetSelectableGroup(selectableGroup);

            formationUnit.transform.position = transform.position;
            formationUnit.transform.rotation = transform.rotation;
            formationUnit.formationHolderPoint = formationHolderPoint.transform;
            // formationUnit.transform.parent = transform;
            unitList.Add(formationUnit.transform.gameObject);
        }

        Transform CreateFormationHolderPoint(int unitNumber)
        {
            List<Vector3> positionList = _formation.GridFormation(
                unitAmount,
                transform.position
            );

            FormationHolderPointBehaviour formationHolderPoint = _formationHolderPointFactory.Create();

            NetworkServer.Spawn(formationHolderPoint.transform.gameObject, connectionToClient);
            
            formationHolderPoint.transform.position = positionList[unitNumber];
            // formationHolderPoint.transform.parent = formationHolderBehaviour.transform;

            return formationHolderPoint.transform;
        }

        public class Factory : PlaceholderFactory<FormationBehaviour> { }
    }
}