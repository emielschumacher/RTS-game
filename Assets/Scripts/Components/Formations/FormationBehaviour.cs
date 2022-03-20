using System.Collections.Generic;
using UnityEngine;
using Game.Components.Formations.Contracts;
using Game.Components.Selections.Selectables;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationBehaviour : NetworkBehaviour
    {
        public SelectableGroup selectableGroup { get; private set; }
        List<GameObject> _unitList = new List<GameObject>();
        [SerializeField] int _unitAmount = 6;
        [SerializeField] GameObject _formationHolderPrefab;
        [SerializeField] GameObject _formationUnitPrefab;
        [SerializeField] GameObject _formationHolderPointPrefab;
        FormationHolderBehaviour _formationHolderBehaviour;
        IFormation _formation = null;

        [Client]
        public override void OnStartClient()
        {
            _formation = this.GetComponent<IFormation>();

            if (!hasAuthority) return;

            selectableGroup = this.GetComponent<SelectableGroup>();

            CmdSpawnFormationUnits();
        }

        [Command]
        void CmdSpawnFormationUnits()
        {
            SpawnFormationHolder();

            for (int unitNumber = 0; unitNumber < _unitAmount; unitNumber++)
            {
                SpawnFormationUnit(unitNumber);
            }
        }

        void SpawnFormationHolder()
        {
            GameObject formationHolder = Instantiate(
                _formationHolderPrefab,
                transform.position,
                transform.rotation
            );

            NetworkServer.Spawn(
                formationHolder.transform.gameObject,
                connectionToClient
            );

            RpcInitSpawnedFormationHolder(formationHolder);
        }

        void SpawnFormationUnit(int unitNumber)
        {
            GameObject formationUnit = Instantiate(
                _formationUnitPrefab,
                transform.position,
                transform.rotation
            );

            NetworkServer.Spawn(
                formationUnit.transform.gameObject,
                connectionToClient
            );

            RpcInitSpawnedFormationUnit(
                formationUnit,
                unitNumber
            );
        }

        [ClientRpc]
        void RpcInitSpawnedFormationHolder(GameObject formationHolder)
        {
            if(!hasAuthority) { return; }

            formationHolder.GetComponent<FormationHolderBehaviour>().formationBehaviour = this;
            _formationHolderBehaviour = formationHolder.GetComponent<FormationHolderBehaviour>();
        }

        [ClientRpc]
        void RpcInitSpawnedFormationUnit(GameObject formationUnit, int unitNumber)
        {
            _unitList.Add(formationUnit.transform.gameObject);

            if (!hasAuthority) { return; }

            SelectableFormationUnit formationUnitSelectable = formationUnit.GetComponent<SelectableFormationUnit>();

            formationUnitSelectable.SetSelectableGroup(selectableGroup);

            Transform formationHolderPoint = SpawnFormationHolderPoint(unitNumber);

            formationUnit.GetComponent<FormationUnitBehaviour>().formationHolderPoint = formationHolderPoint.transform;
        }

        Transform SpawnFormationHolderPoint(int unitNumber)
        {
            List<Vector3> positionList = _formation.GridFormation(
                _unitAmount,
                transform.position
            );

            GameObject formationHolderPoint = Instantiate(
                _formationHolderPointPrefab
            );

            formationHolderPoint.transform.parent = transform;

            formationHolderPoint.transform.position = positionList[unitNumber];
            FormationHolderPointBehaviour formationHolderPointBehaviour = formationHolderPoint.GetComponent<FormationHolderPointBehaviour>();

            formationHolderPointBehaviour.formationHolder = _formationHolderBehaviour;
            Debug.Log(positionList[unitNumber] - transform.position);
            formationHolderPointBehaviour.formationOffset = positionList[unitNumber] - transform.position;

            //NetworkServer.Spawn(
            //    formationHolderPoint.transform.gameObject, connectionToClient
            //);

            return formationHolderPoint.transform;
        }
    }
}