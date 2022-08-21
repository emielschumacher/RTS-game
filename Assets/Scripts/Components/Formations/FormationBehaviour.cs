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

        public void Start()
        {
            _formation = new Formation();
            selectableGroup = this.GetComponent<SelectableGroup>();
        }

        [Client]
        public override void OnStartClient()
        {
            if (!hasAuthority || !isClient) return;

            CmdSpawnFormationUnits();
        }

        [Command]
        void CmdSpawnFormationUnits()
        {
            FormationHolderBehaviour formationHolderBehaviour = SpawnFormationHolder();

            formationHolderBehaviour.formationBehaviour = this;
            
            List<Vector3> formationPositionList = _formation.GridFormation(
                _unitAmount,
                transform.position
            );

            for (int unitNumber = 0; unitNumber < _unitAmount; unitNumber++)
            {
                SpawnFormationUnit(
                    formationHolderBehaviour,
                    formationPositionList[unitNumber]
                );
            }
        }

        FormationHolderBehaviour SpawnFormationHolder()
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

            return formationHolder.GetComponent<FormationHolderBehaviour>();
        }

        void SpawnFormationUnit(
            FormationHolderBehaviour formationHolderBehaviour,
            Vector3 formationPosition
        ) {
            GameObject formationUnit = Instantiate(
                _formationUnitPrefab,
                transform.position,
                transform.rotation
            );

            NetworkServer.Spawn(
                formationUnit.transform.gameObject,
                connectionToClient
            );

            InitSpawnedFormationUnit(
                formationUnit,
                formationHolderBehaviour,
                formationPosition
            );
            RpcInitSpawnedFormationUnit(
                formationUnit,
                formationHolderBehaviour,
                formationPosition
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
            if (!hasAuthority || !isClient) return;

            InitSpawnedFormationHolder(formationHolder);
        }

        void InitSpawnedFormationUnit(
            GameObject formationUnit,
            FormationHolderBehaviour formationHolderBehaviour,
            Vector3 formationPosition
        ) {
            _unitList.Add(formationUnit.transform.gameObject);

            FormationUnitBehaviour formationUnitBehaviour = formationUnit.GetComponent<FormationUnitBehaviour>();
            formationUnitBehaviour.formationHolderBehaviour = formationHolderBehaviour;
            formationUnitBehaviour.transform.position = formationPosition;
            formationUnitBehaviour.formationOffset = formationPosition - transform.position;
            formationUnitBehaviour.localStartPosition = formationHolderBehaviour.transform.InverseTransformPoint(formationPosition);
            
            if (!hasAuthority || !isClient) return;

            SelectableFormationUnit formationUnitSelectable = formationUnit.GetComponent<SelectableFormationUnit>();
            formationUnitSelectable.SetSelectableGroup(selectableGroup);

        }

        [ClientRpc]
        void RpcInitSpawnedFormationUnit(
            GameObject formationUnit,
            FormationHolderBehaviour formationHolderBehaviour,
            Vector3 formationPosition
        ) {
            InitSpawnedFormationUnit(
                formationUnit,
                formationHolderBehaviour,
                formationPosition
            );
        }
    }
}