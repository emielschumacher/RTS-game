using UnityEngine.InputSystem;
using UnityEngine;
using Game.Components.Raycasts.Contracts;
using Game.Components.Raycasts;
using Game.Components.Selections.Selectables;

namespace Game.Components.Selections {
    public class SelectionClickBehaviour : MonoBehaviour
    {
        LayerMask ground;
        IRaycastMousePosition _raycastMousePosition;

        public void Start()
        {
            _raycastMousePosition = new RaycastMousePosition();
        }
        
        void Update()
        {
            if(Mouse.current.leftButton.wasPressedThisFrame) {
                HandleLeftClick();
            }
        }

        private void HandleLeftClick() 
        {
            RaycastHit hit = _raycastMousePosition.GetRaycastHit();

            if(
                !hit.collider.TryGetComponent<AbstractSelectable>(
                    out AbstractSelectable selectable
                )
            ) {
                SelectionManager.instance.DeselectAll();
                return;
            }

            if(Input.GetKey(KeyCode.LeftShift)) {
                SelectionManager.instance.ShiftClickSelect(selectable);
                return;
            }

            SelectionManager.instance.ClickSelect(selectable);
        }
    }
}