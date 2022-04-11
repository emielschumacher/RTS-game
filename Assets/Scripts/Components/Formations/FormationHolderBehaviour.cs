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

        public override void OnStartClient()
        {
            if (!hasAuthority) {
                return;
            }

            NavigationManager
                .instance.navigationMarkerBehaviour
                .onNewMarkerPositionEvent += HandleNewMarkerPositionEvent;
        }

        private void HandleNewMarkerPositionEvent(
            Vector3 destination
        ) {
            if (!hasAuthority) {
                return;
            }
            
            if(formationBehaviour.selectableGroup.isSelected == true) {
                _navigationBehaviour.SetDestination(destination);
            }
        }
    }
}