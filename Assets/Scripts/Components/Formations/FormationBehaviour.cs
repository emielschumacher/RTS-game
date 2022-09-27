using System.Collections.Generic;
using UnityEngine;
using Game.Components.Formations.Contracts;
using Game.Components.Selections.Selectables;
using Mirror;
using Game.Components.Formations.States.Contracts;
using Game.Components.Formations.States;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(SelectableGroup))]
    public class FormationBehaviour : NetworkBehaviour
    {
        public SelectableGroup selectableGroup { get; private set; }
        List<FormationUnitBehaviour> _unitList = new List<FormationUnitBehaviour>();
        [SerializeField] int _unitAmount = 6;
        [SerializeField] GameObject _formationHolderPrefab;
        [SerializeField] GameObject _formationUnitPrefab;
        [SerializeField] GameObject _formationHolderPointPrefab;
        FormationHolderBehaviour _formationHolderBehaviour;
        IFormation _formation = null;

        [SerializeField]
        private IFormationState _currentState;

        public AttackState attackState = new AttackState();

        public void Start()
        {
            _formation = new Formation();
            selectableGroup = this.GetComponent<SelectableGroup>();

            _currentState = new PadrolState();
        }
        
        [ServerCallback]
        void Update()
        {
            _currentState = _currentState.DoState(this);
        }

        public IFormationState GetCurrentState()
        {
            return _currentState;
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
            _unitList.Add(formationUnit.transform.gameObject.GetComponent<FormationUnitBehaviour>());

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

        public void SetFormationState(IFormationState formationState)
        {
            _currentState = formationState;
        }

        public List<FormationUnitBehaviour> GetFormationUnits()
        {
            return _unitList;
        }

        public FormationHolderBehaviour GetFormationHolderBehaviour()
        {
            return _formationHolderBehaviour;
        }
    }
}