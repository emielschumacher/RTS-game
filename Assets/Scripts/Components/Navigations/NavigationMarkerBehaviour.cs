using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Zenject;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Raycasts.Contracts;
using Game.Components.Events;
using Game.Components.Events.Contracts;

namespace Game.Components.Navigations
{
    public class NavigationMarkerBehaviour : MonoBehaviour
    {
        public GameObject groundMarker;
        IRaycastMousePosition _raycastMousePosition;
        [SerializeField] Vector3Event onNewMarkerPositionEvent;

        [Inject]
        public void Construct(
            IRaycastMousePosition raycastMousePosition
        ) {
            _raycastMousePosition = raycastMousePosition;
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