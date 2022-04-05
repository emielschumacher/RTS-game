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

        public void Awake()
        {
            _formation = new Formation();
            selectableGroup = this.GetComponent<SelectableGroup>();
        }

        [Client]
        public override void OnStartClient()
        {
            if (!hasAuthority) return;

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

            InitSpawnedFormationHolder(formationHolder);
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

            Transform formationHolderPoint = SpawnFormationHolderPoint(unitNumber);

            formationUnit.GetComponent<FormationUnitBehaviour>().formationHolderPoint = formationHolderPoint.transform;

            InitSpawnedFormationUnit(
                formationUnit,
                unitNumber,
                formationHolderPoint
            );
            RpcInitSpawnedFormationUnit(
                formationUnit,
                unitNumber,
                formationHolderPoint
            );
        }

        Transform SpawnFormationHolderPoint(int unitNumber)
        {
            GameObject formationHolderPoint = Instantiate(
                _formationHolderPointPrefab
            );

            NetworkServer.Spawn(
                formationHolderPoint.transform.gameObject, connectionToClient
            );

            List<Vector3> positionList = _formation.GridFormation(
                _unitAmount,
                transform.position
            );

            InitFormationHolderPoint(
                formationHolderPoint,
                positionList[unitNumber]
            );
            RpcInitFormationHolderPoint(
                formationHolderPoint,
                positionList[unitNumber]
            );

            return formationHolderPoint.transform;
        }

        void InitFormationHolderPoint(
            GameObject formationHolderPoint,
            Vector3 spawnPosition)
        {
            //formationHolderPoint.transform.parent = transform;
            formationHolderPoint.transform.position = spawnPosition;
            FormationHolderPointBehaviour formationHolderPointBehaviour = formationHolderPoint.GetComponent<FormationHolderPointBehaviour>();
            formationHolderPointBehaviour.formationHolder = _formationHolderBehaviour;
            formationHolderPointBehaviour.formationOffset = spawnPosition - transform.position;
        }

        [ClientRpc]
        void RpcInitFormationHolderPoint(
            GameObject formationHolderPoint,
            Vector3 spawnPosition
        ) {
            InitFormationHolderPoint(
                formationHolderPoint,
                spawnPosition
            );
        }

        void InitSpawnedFormationHolder(GameObject formationHolder)
        {
            formationHolder.GetComponent<FormationHolderBehaviour>().formationBehaviour = this;
            _formationHolderBehaviour = formationHolder.GetComponent<FormationHolderBehaviour>();
        }

        [ClientRpc]
        void RpcInitSpawnedFormationHolder(GameObject formationHolder)
        {
            //if(!hasAuthority) { return; }

            InitSpawnedFormationHolder(formationHolder);
        }

        void InitSpawnedFormationUnit(
            GameObject formationUnit,
            int unitNumber,
            Transform formationHolderPoint
        ) {
            _unitList.Add(formationUnit.transform.gameObject);

            if (!hasAuthority) { return; }

            SelectableFormationUnit formationUnitSelectable = formationUnit.GetComponent<SelectableFormationUnit>();

            formationUnitSelectable.SetSelectableGroup(selectableGroup);

            formationUnit.GetComponent<FormationUnitBehaviour>().formationHolderPoint = formationHolderPoint.transform;
        }

        [ClientRpc]
        void RpcInitSpawnedFormationUnit(
            GameObject formationUnit,
            int unitNumber,
            Transform formationHolderPoint
        ) {
            InitSpawnedFormationUnit(
                formationUnit,
                unitNumber,
                formationHolderPoint
            );
        }
    }
}