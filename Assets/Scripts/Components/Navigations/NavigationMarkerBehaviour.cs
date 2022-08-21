using UnityEngine;
using Game.Components.Raycasts.Contracts;
using Game.Components.Raycasts;
using UnityEngine.Events;
using Game.Components.Selections;

namespace Game.Components.Navigations
{
    public class NavigationMarkerBehaviour : MonoBehaviour
    {
        public GameObject groundMarker;
        IRaycastMousePosition _raycastMousePosition;
        public UnityAction<Vector3> onNewMarkerPositionEvent;

        public void Awake() {
            _raycastMousePosition = new RaycastMousePosition();
        }

        void Update() {
            if(Input.GetMouseButtonDown(0))
                HandleLeftClick();
            if(Input.GetMouseButtonDown(1))
                HandleRightClick();
        }

        void HandleLeftClick() {
            groundMarker.SetActive(false);
        }

        void HandleRightClick() {
            if(!SelectionManager.instance.HasSelectedObjects())
            {
                return;
            }

            RaycastHit hit = _raycastMousePosition.GetRaycastHit();

            groundMarker.transform.position = hit.point;
            groundMarker.SetActive(true);
            onNewMarkerPositionEvent?.Invoke(hit.point);
        }
    }
}