using UnityEngine.InputSystem;
using UnityEngine;
using Game.Components.Selections.Selectables;
using Game.Components.Raycasts.Contracts;
using Game.Components.Raycasts;

namespace Game.Components.Selections {
    public class SelectionClickBehaviour : MonoBehaviour
    {
        LayerMask ground;
        SelectionManager _selectionManager;
        IRaycastMousePosition _raycastMousePosition;

        public void Start()
        {
            _selectionManager = GetComponent<SelectionManager>();
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
                _selectionManager.DeselectAll();
                return;
            }

            if(Input.GetKey(KeyCode.LeftShift)) {
                _selectionManager.ShiftClickSelect(selectable);
                return;
            }
            
            _selectionManager.ClickSelect(selectable);
        }
    }
}