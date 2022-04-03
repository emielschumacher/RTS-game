using UnityEngine;
using Game.Components.Navigations;
using Mirror;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    public class FormationHolderBehaviour : NetworkBehaviour
    {
        public FormationBehaviour formationBehaviour;
        private NavigationBehaviour _navigationBehaviour;

        public void Start()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();

            if (hasAuthority) {
                NavigationManager
                    .instance.navigationMarkerBehaviour
                    .onNewMarkerPositionEvent += HandleNewMarkerPositionEvent;
            }
        }

        private void HandleNewMarkerPositionEvent(
            Vector3 destination
        ) {
            if (!hasAuthority) {
                Debug.Log("no auth!");
                return;
                }

            if(formationBehaviour.selectableGroup.isSelected == true) {
                _navigationBehaviour.SetDestination(destination);
            }
        }
    }
}