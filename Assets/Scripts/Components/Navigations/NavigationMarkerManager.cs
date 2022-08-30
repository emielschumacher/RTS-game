using UnityEngine;
using UnityEngine.Events;
using Game.Components.Selections;

namespace Game.Components.Navigations
{
    public class NavigationMarkerManager : MonoBehaviour
    {
        public static NavigationMarkerManager instance { get; private set; }

        public GameObject groundMarker;
        public UnityAction<Vector3> onNewMarkerPositionEvent;

        private void Start()
        {
            if (instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }

        void Update() {
            if(Input.GetMouseButtonDown(0))
                HandleLeftClick();
        }

        void HandleLeftClick() {
            ResetMarker();
        }

        public void ResetMarker()
        {
            groundMarker.SetActive(false);
        }

        public void TrySetNewNavigationMarker(RaycastHit hit) {
            if(!SelectionManager.instance.HasSelectedObjects())
            {
                return;
            }

            groundMarker.transform.position = hit.point;
            groundMarker.SetActive(true);
            onNewMarkerPositionEvent?.Invoke(hit.point);
        }
    }
}