using UnityEngine;
using Game.Components.Navigations;
using Mirror;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    public class FormationHolderBehaviour : NetworkBehaviour
    {
        public FormationBehaviour formationBehaviour;
        [SerializeField]private NavigationBehaviour _navigationBehaviour;

        public void Start()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();
        }
        
        public void HandleNewMarkerPositionEvent(
            Vector3 destination
        ) {
            if(formationBehaviour.selectableGroup.isSelected == true) {
                _navigationBehaviour.SetDestination(destination);
            }
        }
    }
}