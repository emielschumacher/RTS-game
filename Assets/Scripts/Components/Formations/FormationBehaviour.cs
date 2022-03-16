using System.Collections.Generic;
using UnityEngine;
using Game.Components.Formations.Contracts;
using Game.Components.Selections.Selectables;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationBehaviour : NetworkBehaviour
    {
        private List<GameObject> _unitList = new List<GameObject>();
        [SerializeField] private int _unitAmount = 6;
        [SerializeField] private GameObject _formationHolderPrefab;
        [SerializeField] private GameObject _formationUnitPrefab;
        [SerializeField] private GameObject _formationHolderPointPrefab;
        public SelectableGroup selectableGroup { get; private set; }
        private FormationHolderBehaviour _formationHolderBehaviour;
        private IFormation _formation = null;

        public void Start()
        {
            _formation = this.GetComponent<IFormation>();

            if (!hasAuthority || !isClient) return;

            base.OnStartClient();

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
            Transform formationHolderPoint = SpawnFormationHolderPoint(unitNumber);

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
                formationHolderPoint.gameObject
            );
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

            formationHolderPoint.transform.position = positionList[unitNumber];

            NetworkServer.Spawn(
                formationHolderPoint.transform.gameObject, connectionToClient
            );

            return formationHolderPoint.transform;
        }

        [ClientRpc]
        void RpcInitSpawnedFormationHolder(GameObject formationHolder)
        {
            formationHolder.GetComponent<FormationHolderBehaviour>().formationBehaviour = this;

            _formationHolderBehaviour = formationHolder.GetComponent<FormationHolderBehaviour>();
        }

        [ClientRpc]
        void RpcInitSpawnedFormationUnit(GameObject formationUnit, GameObject formationHolderPoint)
        {
            SelectableFormationUnit formationUnitSelectable = formationUnit.GetComponent<SelectableFormationUnit>();

            formationUnitSelectable.SetSelectableGroup(selectableGroup);
            formationUnit.GetComponent<FormationUnitBehaviour>().formationHolderPoint = formationHolderPoint.transform;
            _unitList.Add(formationUnit.transform.gameObject);
        }
    }
}