using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Raycasts.Contracts;
using Game.Components.Raycasts;
using Mirror;

namespace Game.Components.Selections {
    public class SelectionClickBehaviour : NetworkBehaviour
    {
        LayerMask ground;
        ISelectionManager _selectionManager;
        IRaycastMousePosition _raycastMousePosition;

        public void Awake() {
            _selectionManager = new SelectionManager();
            _raycastMousePosition = new RaycastMousePosition();
        }
        
        [ClientCallback]
        void Update()
        {
            if (!isLocalPlayer) return;

            if(Mouse.current.leftButton.wasPressedThisFrame) {
                HandleLeftClick();
            }
        }

        [Command]
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