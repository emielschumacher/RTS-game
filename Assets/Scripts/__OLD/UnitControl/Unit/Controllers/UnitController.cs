using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitController : MonoBehaviour
{
    public GameObject selectIndicator;
    public Transform formationHolderPointTransform;
    public AnimationsController animationsController;
    public GameObject unitGroup;
    private GameObject _unitGroupFormationHolder;
    private NavMeshAgent _navMeshAgent;
    private NavMeshAgent _unitGroupFormationHolderNavMeshAgent;
    private FormationHolderController _unitGroupFormationHolderController;
    private UnitGroupController _unitGroupController;
    private Vector3 _lastPosition;

    private void Awake()
    {
        if(transform.parent.gameObject.tag == Tags.unitGroupTag) {
            unitGroup = transform.parent.gameObject;
        }
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _unitGroupController = unitGroup.GetComponent<UnitGroupController>();
        _unitGroupFormationHolder = _unitGroupController.formationHolder;
        _unitGroupFormationHolderNavMeshAgent = _unitGroupFormationHolder.GetComponent<NavMeshAgent>();
        _unitGroupFormationHolderController = _unitGroupFormationHolder.GetComponent<FormationHolderController>();

        _unitGroupController.unitList.Add(this.gameObject);
        SelectionController.Instance.unitList.Add(this.gameObject);
        
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
    }

    private void Update() {
        if(!formationHolderPointTransform) {
            return;
        }

        _SmoothRotation();
        _SmoothMovement();
        _MoveAnimation();
    }

    private void _MoveAnimation() 
    {
        float distance = Vector3.Distance(transform.position, formationHolderPointTransform.position);
        
        animationsController.SetMovingState(_IsMoving() && distance > 0.2f);
    }

    private bool _IsMoving()
    {
        Vector3 displacement = transform.position - _lastPosition;
        _lastPosition = transform.position;

        return displacement.magnitude > 0.001;
    }

    private void _SmoothMovement()
     {
        _navMeshAgent.SetDestination(formationHolderPointTransform.position);

        transform.position = Vector3.Lerp(
            transform.position,
            _navMeshAgent.nextPosition,
            8f * Time.deltaTime
        );
    }

    private void _SmoothRotation ()
    {
        Quaternion rotation = Quaternion.identity;
        
        if(_navMeshAgent.nextPosition == Vector3.zero || !HasRemainingDistance()) {
            rotation = _unitGroupFormationHolder.transform.rotation;

            if(_unitGroupFormationHolderController.formationRotationHelper.angleForward > 90) {
                rotation *= Quaternion.Euler(0, +180, 0);
            }
        } else {
            if((_navMeshAgent.nextPosition - transform.position) == Vector3.zero) {
                return;
            }

            rotation = Quaternion.LookRotation(_navMeshAgent.nextPosition - transform.position);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation,rotation, 5f * Time.deltaTime);
    }

    private bool HasRemainingDistance() {
        return !(!_navMeshAgent.pathPending
            && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance
            && !_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f);
    }

    private void OnDestroy() {
        if (Time.frameCount == 0) return;

        // call event here!
        
        SelectionController.Instance.unitList.Remove(this.gameObject);
        _unitGroupController.unitList.Remove(this.gameObject);
        _unitGroupFormationHolderController.GeneratePoints();
    }
}
