using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Raycasts.Contracts;
using Zenject;

namespace Game.Components.Selections {
    public class SelectionClickBehaviour : MonoBehaviour
    {
        LayerMask ground;
        ISelectionManager _selectionManager;
        IRaycastMousePosition _raycastMousePosition;

        [Inject]
        public void Construct(
            ISelectionManager selectionManager,
            IRaycastMousePosition raycastMousePosition
        ) {
            _selectionManager = selectionManager;
            _raycastMousePosition = raycastMousePosition;
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