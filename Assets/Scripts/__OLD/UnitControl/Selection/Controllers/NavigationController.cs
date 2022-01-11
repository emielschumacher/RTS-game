using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationController : MonoBehaviour
{
    public  LayerMask groundLayer;
    private Camera _camera;
    private NavMeshAgent _navMeshAgent;
    private bool _hasSetDestinations;
    private Vector3 _hitPointPosition;

    void Start()
    {
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _HandleGroundClick();
    }

    private void _HandleGroundClick()
    {
        if(!Input.GetMouseButtonDown(1)) {
            return;
        }

        _hitPointPosition = _GetRayCastHitPoint();

        if(_hasSetDestinations == false && _hitPointPosition != Vector3.zero) {
            _SetUnitGroupsDestenations(_hitPointPosition);
        }
    }

    private Vector3 _GetRayCastHitPoint()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(!Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) {
            return Vector3.zero;
        }

        return hit.point;
    }

    private void _SetUnitGroupsDestenations(Vector3 hitPointPosition) 
    {
        List<GameObject> selectedUnitGroups = SelectionController.Instance.selectedUnitGroups;
        List<Vector3> positionList = _SquareFormation(selectedUnitGroups.Count);
        int i = 0;

        foreach(var selectedUnitGroup in selectedUnitGroups) {
            GameObject formationHolder = selectedUnitGroup.GetComponent<UnitGroupController>().formationHolder;
            NavMeshAgent formationHolderAgent = formationHolder.GetComponent<NavMeshAgent>();

            formationHolderAgent.SetDestination(hitPointPosition + positionList[i]);
            i++;
        }
    }

    // private Vector3 _GetClosestAvailableUnitGroup (List<Vector3> enemies, Vector3 formationPoint)
    // {
    //     Vector3 bestTarget = Vector3.zero;
    //     float closestDistanceSqr = Mathf.Infinity;
    //     Vector3 currentPosition = formationPoint;

    //     foreach(Vector3 potentialTarget in enemies)
    //     {
    //         Vector3 directionToTarget = potentialTarget - currentPosition;
    //         float dSqrToTarget = directionToTarget.sqrMagnitude;

    //         if(dSqrToTarget < closestDistanceSqr)
    //         {
    //             closestDistanceSqr = dSqrToTarget;
    //             bestTarget = potentialTarget;
    //         }
    //     }

    //     return bestTarget;
    // }

    private List<Vector3> _SquareFormation(int unitGroupsAmount)
    {
        List<Vector3> positionList = new List<Vector3>();
        
        Vector3 targetpostion = Vector3.zero;

        int counter = -1;
        int xoffset = -1;
        float sqrt = Mathf.Sqrt(10);
        float startx = targetpostion.x;

        for (int i = 0; i < unitGroupsAmount; i++)
        {
            counter++;
            xoffset++;

            if (xoffset > 1)
            {
                xoffset = 1;
            }

            targetpostion = new Vector3(targetpostion.x + (xoffset * 7f), targetpostion.z, 0f);

            if (counter == Mathf.Floor(sqrt))
            {
                counter = 0;
                targetpostion.x = startx;
                targetpostion.z += 1 + 7f;
            }
            
            positionList.Add(targetpostion);
        }

        return positionList;
    }

    // private Vector3 FindCentroid ( List< GameObject > targets )
    //  {
    //     var bound = new Bounds(targets[0].transform.position, Vector3.zero);
    //     for(int i = 1; i < targets.Count; i++) {
    //         bound.Encapsulate(targets[i].transform.position);
    //     }
    //     return bound.center;
    // }  
}
