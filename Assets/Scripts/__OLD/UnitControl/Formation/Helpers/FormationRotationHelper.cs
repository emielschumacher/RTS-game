using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationRotationHelper
{
    [HideInInspector] public float angleForward;
    
    public void RotateTowardsTarget(
        Vector3 target,
        Transform transform
    ) {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        angleForward = Vector3.Angle(direction, transform.forward); 

        if(angleForward >= 90) {
            rotation *= Quaternion.Euler(0, -180, 0);
        }

        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

}
