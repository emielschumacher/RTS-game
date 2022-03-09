using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Raycasts.Contracts;
using Game.Components.Raycasts;
using Game.Components.Events;
using Game.Components.Events.Contracts;
using Mirror;

namespace Game.Components.Navigations
{
    public class NavigationMarkerBehaviour : NetworkBehaviour
    {
        public GameObject groundMarker;
        IRaycastMousePosition _raycastMousePosition;
        [SerializeField] Vector3Event onNewMarkerPositionEvent;

        public void Awake() {
            _raycastMousePosition = new RaycastMousePosition();
        }

        [ClientCallback]
        void Update() {
            if (!isLocalPlayer) return;

            if(Input.GetMouseButtonDown(0))
                HandleLeftClick();
            if(Input.GetMouseButtonDown(1))
                HandleRightClick();
        }

        [Client]
        void HandleLeftClick() {
            groundMarker.SetActive(false);
        }

        [Client]
        void HandleRightClick() {
            RaycastHit hit = _raycastMousePosition.GetRaycastHit();

            groundMarker.transform.position = hit.point;
            groundMarker.SetActive(true);
            onNewMarkerPositionEvent?.Raise(hit.point);
        }
    }
}