using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Components.Selections.Selectables.Contracts;
using Game.Components.Selections.Contracts;
using Game.Components.Navigations;

namespace Game.Components.Navigations
{
    public class NavigationManager : MonoBehaviour
    {
        // public void SetDestination(Vector3 destination)
        // {
        //     List<ISelectable> selectedList = _selectionManager.GetSelectedList();

        //     foreach(ISelectable selectable in selectedList) {
        //         if(!(selectable is ISelectableFormationUnit)) continue;

        //         ISelectableFormationUnit selectableFormationUnit = selectable as ISelectableFormationUnit;
                
        //         selectableFormationUnit
        //             .formationUnitBehaviour
        //             .formationHolderBehaviour
        //             .SetDestination(destination)
        //         ;
        //     }
        // }
    }
}