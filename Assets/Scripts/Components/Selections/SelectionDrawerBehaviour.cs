using UnityEngine;
using Game.Components.Networking;
using UnityEngine.InputSystem;
using Mirror;
using Game.Components.Selections.Selectables.Contracts;

namespace Game.Components.Selections
{
    public class SelectionDrawerBehaviour : MonoBehaviour
    {
        Camera _camera;
        [SerializeField] RectTransform _boxVisual;
        Rect _selectionBox;
        Vector2 _startPosition;
        Vector2 _endPosition;
        MyNetworkPlayer _myNetworkPlayer;

        void Start()
        {
            _camera = Camera.main;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;

            DrawVisual();
        }

        void Update()
        {
            //// TEMPORARY: Because network player join's to late switching from menu scene and we don't want to use this as a NetworkBehaviour
            //if (_myNetworkPlayer == null)
            //{
            //    // Since we are not NetworkBehaviour and can't use [ClientCallback]:
            //    if (!NetworkClient.connection.isReady)
            //    {
            //        Debug.Log("Conn not ready!");
            //        return;
            //    }

            //    _myNetworkPlayer = NetworkClient.connection.identity.GetComponent<MyNetworkPlayer>();
            //}


            if (Mouse.current.leftButton.wasPressedThisFrame) {
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
            //foreach (var formationUnit in _myNetworkPlayer.GetMyFormationUnits())
            //{
            //    ISelectable selectable = formationUnit.GetComponent<ISelectable>();

            //    if (!_selectionBox.Contains(
            //        _camera.WorldToScreenPoint(selectable.transform.position)
            //    )) continue;

            //    SelectionManager.instance.DragSelect(selectable);
            //}
        }
    }
}