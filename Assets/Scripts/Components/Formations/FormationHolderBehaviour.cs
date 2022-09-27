using UnityEngine;
using Game.Components.Navigations;
using Mirror;
using Game.Components.Targets;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    [RequireComponent(typeof(Targeter))]
    public class FormationHolderBehaviour : NetworkBehaviour
    {
        public FormationBehaviour formationBehaviour;
        [SerializeField] private NavigationBehaviour _navigationBehaviour;
        [SerializeField] private Targeter _targeter;

        public void Start()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();
            _targeter = this.GetComponent<Targeter>();
        }
        
        [Client]
        public void HandleNewMarkerPositionEvent(
            Vector3 destination
        ) {
            if(formationBehaviour.selectableGroup.isSelected == true) {
                _targeter.CmdClearTarget();
                _navigationBehaviour.CmdSetDestination(destination);
            }
        }

        public Targeter GetTargeter()
        {
            return _targeter;
        }

        public NavigationBehaviour GetNavigationBehaviour()
        {
            return _navigationBehaviour;
        }
    }
}