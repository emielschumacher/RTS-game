using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    private Camera myCam;
    public GameObject groundMarker;
    public LayerMask clickable;
    public LayerMask ground;

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            SelectUnits();
        }

        if(Input.GetMouseButtonDown(1)) {
            PlaceGroundMarker();
        }
    }

    private void SelectUnits() 
    {
        RaycastHit hit = GetRayCastHit(clickable);

        if(hit.point == Vector3.zero) {
            SelectionController.Instance.DeselectAll();
            return;
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            SelectionController.Instance.ShiftClickSelect(hit.collider.gameObject);
            return; 
        } 
        
        SelectionController.Instance.ClickSelect(hit.collider.gameObject);
    }

    private void PlaceGroundMarker() {
        RaycastHit hit = GetRayCastHit(ground);

        if(hit.point == Vector3.zero) {
            SelectionController.Instance.DeselectAll();
        }

        groundMarker.transform.position = hit.point;
        groundMarker.SetActive(false);
        groundMarker.SetActive(true);
    }

    private RaycastHit GetRayCastHit(LayerMask layerMask)
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        
        return hit;
    }
}
