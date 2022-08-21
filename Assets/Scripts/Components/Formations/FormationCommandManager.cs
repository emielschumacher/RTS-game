using UnityEngine;
using Game.Components.Navigations;
using Game.Components.Selections.Selectables;
using Game.Components.Selections;
using Game.Components.Formations;

public class FormationCommandManager : MonoBehaviour
{
    public void Start()
    {
        NavigationManager
            .instance.navigationMarkerBehaviour
            .onNewMarkerPositionEvent += HandleNewNavigationMarkerPositionEvent;
    }

    private void HandleNewNavigationMarkerPositionEvent(
        Vector3 destination
    ) {
        foreach(AbstractSelectable selectedFormationUnit in SelectionManager.instance.selectedFormationUnits)
        {
            selectedFormationUnit
                .GetComponent<FormationUnitBehaviour>()
                .formationHolderBehaviour
                .HandleNewMarkerPositionEvent(destination)
            ;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleLeftClick();
        if (Input.GetMouseButtonDown(1))
            HandleRightClick();
    }

    void HandleLeftClick()
    {
    }

    void HandleRightClick()
    {
    }
}
