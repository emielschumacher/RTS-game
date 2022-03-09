using System.Collections.Generic;
using UnityEngine;
using Game.Components.Formations;
using Game.Components.Formations.Contracts;
using Game.Components.Selections.Selectables.Contracts;
using Game.Components.Selections.Selectables;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationBehaviour : NetworkBehaviour
    {
        public List<GameObject> unitList = new List<GameObject>();
        [HideInInspector] public FormationHolderBehaviour formationHolderBehaviour;
        public int unitAmount;
        [HideInInspector] public SelectableGroup selectableGroup;
        IFormation _formation;
        public GameObject formationHolderPrefab;
        public GameObject formationUnitPrefab;
        public GameObject formationHolderPointPrefab;

        void Awake() {
            _formation = new Formation();
        }

        public void Start()
        {
            base.OnStartClient();

            selectableGroup = this.GetComponent<SelectableGroup>();

            CmdSpawnFormationUnits();
        }

        [Command]
        void CmdSpawnFormationUnits()
        {
            SpawnFormationHolder();

            for (int unitNumber = 0; unitNumber < unitAmount; unitNumber++)
            {
                SpawnFormationUnit(unitNumber);
            }
        }

        void SpawnFormationHolder()
        {
            GameObject formationHolder = Instantiate(
                formationHolderPrefab,
                transform.position,
                transform.rotation
            );

            NetworkServer.Spawn(formationHolder.transform.gameObject, connectionToClient);

            RpcInitSpawnedFormationHolder(formationHolder);
        }

        [ClientRpc]
        void RpcInitSpawnedFormationHolder(GameObject formationHolder)
        {
            formationHolder.GetComponent<FormationHolderBehaviour>().formationBehaviour = this;
            
            formationHolderBehaviour = formationHolder.GetComponent<FormationHolderBehaviour>();
        }

        void SpawnFormationUnit(int unitNumber)
        {
            Transform formationHolderPoint = SpawnFormationHolderPoint(unitNumber);

            GameObject formationUnit = Instantiate(
                formationUnitPrefab,
                transform.position,
                transform.rotation
            );

            NetworkServer.Spawn(formationUnit.transform.gameObject, connectionToClient);

            RpcInitSpawnedFormationUnit(formationUnit, formationHolderPoint.gameObject);
        }

        [ClientRpc]
        void RpcInitSpawnedFormationUnit(GameObject formationUnit, GameObject formationHolderPoint)
        {
            SelectableFormationUnit formationUnitSelectable = formationUnit.GetComponent<SelectableFormationUnit>();

            formationUnitSelectable.SetSelectableGroup(selectableGroup);

            formationUnit.GetComponent<FormationUnitBehaviour>().formationHolderPoint = formationHolderPoint.transform;
            // formationUnit.transform.parent = transform;
            unitList.Add(formationUnit.transform.gameObject);
        }

        Transform SpawnFormationHolderPoint(int unitNumber)
        {
            List<Vector3> positionList = _formation.GridFormation(
                unitAmount,
                transform.position
            );

            GameObject formationHolderPoint = Instantiate(
                formationHolderPointPrefab
            );

            formationHolderPoint.transform.position = positionList[unitNumber];

            NetworkServer.Spawn(formationHolderPoint.transform.gameObject, connectionToClient);

            return formationHolderPoint.transform;
        }

        [ClientRpc]
        void InitSpawnedFormationHolderPoint(GameObject formationUnit)
        {
            
        }
    }
}