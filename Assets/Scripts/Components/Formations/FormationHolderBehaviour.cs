using System.Collections.Generic;
using UnityEngine;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Selections;

namespace Game.Components.Formations
{
    [RequireComponent(typeof(NavigationBehaviour))]
    public class FormationHolderBehaviour : MonoBehaviour
    {
        public FormationBehaviour formationBehaviour;
        NavigationBehaviour _navigationBehaviour;

        void Awake() {
            _navigationBehaviour = GetComponent<NavigationBehaviour>();   
        }
        
        public void SetDestination(Vector3 destination)
        {
            if(formationBehaviour.selectableGroup.isSelected == true) {
                _navigationBehaviour.SetDestination(destination);
            }
        }
    }
}