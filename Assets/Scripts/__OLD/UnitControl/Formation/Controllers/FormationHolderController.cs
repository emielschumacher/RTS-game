using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FormationHolderController : MonoBehaviour
{
    public GameObject formationHolderPointPrefab;
    [HideInInspector] public GameObject unitGroup;
    private UnitGroupController _unitGroupController;
    private NavMeshAgent _navMeshAgent;
    private LineRenderer _lineRenderer;
    [HideInInspector] public FormationRotationHelper formationRotationHelper;
    
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _unitGroupController = unitGroup.GetComponent<UnitGroupController>();
        formationRotationHelper = new FormationRotationHelper();

        _navMeshAgent.updateRotation = false;
    }

    void Update()
    {
        if(HasRemainingDistance()) {
            formationRotationHelper.RotateTowardsTarget(
                _navMeshAgent.steeringTarget,
                transform
            );
        }
    }

    private bool HasRemainingDistance() {
        return !(!_navMeshAgent.pathPending
            && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance
            && !_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f);
    }

    public void GeneratePoints()
    {
        List<Vector3> positionList = FormationHelper.SquareFormation(
            _unitGroupController.unitAmount,
            transform.position
        );
        int i = 0;

        foreach(GameObject unit in _unitGroupController.unitList) {
            GameObject instance = Instantiate(
                formationHolderPointPrefab,
                positionList[i],
                Quaternion.identity,
                transform
            );
            unit.GetComponent<UnitController>().formationHolderPointTransform = instance.transform;
            i++;
        }
    }

    // void DrawPath() {
    //     myLineRenderer.positionCount = navMeshAgent.path.corners.Length;
    //     myLineRenderer.SetPosition(0, transform.position);

    //     if(navMeshAgent.path.corners.Length < 2) {
    //         return;
    //     }

    //     for(int i = 1; i < navMeshAgent.path.corners.Length; i++) {
    //         Vector3 pointPosition = new Vector3(
    //             navMeshAgent.path.corners[i].x,
    //             navMeshAgent.path.corners[i].y,
    //             navMeshAgent.path.corners[i].z
    //         );

    //         myLineRenderer.SetPosition(i, pointPosition);
    //     }
    // }
}
