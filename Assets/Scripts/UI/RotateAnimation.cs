using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    public float speed = 50f;

    void Update ()
    {
        transform.Rotate(0,0, speed * Time.deltaTime);
    }
}
