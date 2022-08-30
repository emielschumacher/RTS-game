using UnityEngine;
using Game.Components.Navigations;
using Game.Components.Selections.Selectables;
using Game.Components.Selections;
using Game.Components.Formations;
using Game.Components.Raycasts;
using Game.Components.Targets;

public class FormationCommandManager : MonoBehaviour
{
    public static FormationCommandManager instance { get; private set; }

    RaycastMousePosition _raycastMousePosition = new RaycastMousePosition();

    public void Start()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }

        NavigationMarkerManager
            .instance
            .onNewMarkerPositionEvent += HandleNewNavigationMarkerPositionEvent;
    }

    private void HandleNewNavigationMarkerPositionEvent(
        Vector3 destination
    ) {
        foreach(
            AbstractSelectable selectedFormationUnit
            in SelectionManager.instance.selectedFormationUnits
        ) {
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
        NavigationMarkerManager.instance.ResetMarker();
    }

    void HandleRightClick()
    {
        RaycastHit hit = _raycastMousePosition.GetRaycastHit();

        NavigationMarkerManager.instance.ResetMarker();

        if (
            hit.collider.TryGetComponent<Targetable>(out Targetable target)
            && !target.hasAuthority
        ) {
            TargetManager.instance.TrySetTarget(target);

            return;
        }

        NavigationMarkerManager.instance.TrySetNewNavigationMarker(hit);
    }
}
