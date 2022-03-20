using UnityEngine;
using Game.Components.Raycasts.Contracts;
using Game.Components.Raycasts;
using Game.Components.Events;

namespace Game.Components.Navigations
{
    public class NavigationMarkerBehaviour : MonoBehaviour
    {
        public GameObject groundMarker;
        IRaycastMousePosition _raycastMousePosition;
        [SerializeField] Vector3Event onNewMarkerPositionEvent;

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
            RaycastHit hit = _raycastMousePosition.GetRaycastHit();

            groundMarker.transform.position = hit.point;
            groundMarker.SetActive(true);
            onNewMarkerPositionEvent?.Raise(hit.point);
        }
    }
}