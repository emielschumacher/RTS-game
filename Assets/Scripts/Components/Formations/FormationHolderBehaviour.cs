using System.Collections.Generic;
using UnityEngine;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Selections;
using Mirror;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    public class FormationHolderBehaviour : NetworkBehaviour
    {
        [HideInInspector] public FormationBehaviour formationBehaviour;
        NavigationBehaviour _navigationBehaviour;

        [Client]
        public override void OnStartClient()
        {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();   
        }
        
        [Client]
        public void SetDestination(Vector3 destination)
        {
            // if (!isLocalPlayer || !hasAuthority) return;
            
            if(formationBehaviour.selectableGroup.isSelected == true) {
                _navigationBehaviour.SetDestination(destination);
            }
        }
    }
}