using UnityEngine;
using Game.Components.Navigations;
using Mirror;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    public class FormationHolderBehaviour : NetworkBehaviour
    {
        [HideInInspector] public FormationBehaviour formationBehaviour;
        private NavigationBehaviour _navigationBehaviour;

        [Client]
        public override void OnStartClient()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();

            NavigationManager
                .instance.navigationMarkerBehaviour
                .onNewMarkerPositionEvent += HandleNewMarkerPositionEvent;
        }

        private void HandleNewMarkerPositionEvent(
            Vector3 position
        ) {
            SetDestination(position);
        }

        [Client]
        public void SetDestination(Vector3 destination)
        {
            if(formationBehaviour.selectableGroup.isSelected == true) {
                _navigationBehaviour.SetDestination(destination);
            }
        }
    }
}