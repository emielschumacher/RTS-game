using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Selections.Selectables.Contracts;
using Game.Components.Scenes.Contracts;
using Game.Components.Networking;
using UnityEngine.InputSystem;
using Zenject;
using Mirror;

namespace Game.Components.Selections
{
    public class SelectionDrawerBehaviour : NetworkBehaviour
    {
        Camera _camera;
        [SerializeField] RectTransform _boxVisual;
        Rect _selectionBox;
        Vector2 _startPosition;
        Vector2 _endPosition;
        MyNetworkPlayer _myNetworkPlayer;
        ISelectionManager _selectionManager;

        [Inject]
        public void Construct(
            ISelectionManager selectionManager
        ) {
            _selectionManager = selectionManager;
        }

        void Start()
        {
            _camera = Camera.main;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;

            DrawVisual();
        }

        void Update()
        {
            if(_myNetworkPlayer == null) {
                _myNetworkPlayer = NetworkClient
                    .connection
                    .identity
                    .GetComponent<MyNetworkPlayer>()
                ;
            }

            if(Mouse.current.leftButton.wasPressedThisFrame) {
                _startPosition = Input.mousePosition;
                _selectionBox = new Rect();
            }

            if(Mouse.current.leftButton.isPressed) {
                _endPosition = Input.mousePosition;
                DrawVisual();
                DrawSelection();
            }

            if(Mouse.current.leftButton.wasReleasedThisFrame) {
                SelectGameObjects();
                _startPosition = Vector2.zero;
                _endPosition = Vector2.zero;
                DrawVisual();
            }
        }

        void DrawVisual()
        {
            Vector2 boxStart = _startPosition;
            Vector2 boxEnd = _endPosition;
            Vector2 boxCenter = (boxStart + boxEnd) / 2;

            _boxVisual.position = boxCenter;

            Vector2 boxSize = new Vector2(
                Mathf.Abs(boxStart.x - boxEnd.x),
                Mathf.Abs(boxStart.y - boxEnd.y)
            );
            
            _boxVisual.sizeDelta = boxSize;
        }

        void DrawSelection()
        {
            if(Input.mousePosition.x < _startPosition.x) {
                _selectionBox.xMin = Input.mousePosition.x;
                _selectionBox.xMax = _startPosition.x;
            } else {
                _selectionBox.xMin = _startPosition.x;
                _selectionBox.xMax = Input.mousePosition.x;
            }

            if(Input.mousePosition.y < _startPosition.y) {
                _selectionBox.yMin = Input.mousePosition.y;
                _selectionBox.yMax = _startPosition.y;
            } else {
                _selectionBox.yMin = _startPosition.y;
                _selectionBox.yMax = Input.mousePosition.y;
            }
        }

        void SelectGameObjects()
        {
            foreach(var formationUnit in _myNetworkPlayer.GetMyFormationUnits())
            {
                AbstractSelectable selectable = formationUnit.GetComponent<AbstractSelectable>();
                
                if(!_selectionBox.Contains(
                    _camera.WorldToScreenPoint(selectable.transform.position)
                )) continue;

                _selectionManager.DragSelect(selectable);
            }
        }
    }
}