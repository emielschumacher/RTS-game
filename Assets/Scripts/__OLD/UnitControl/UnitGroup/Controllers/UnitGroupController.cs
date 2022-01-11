using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroupController : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public GameObject formationHolder;
    public GameObject unitPrefab;
    public int unitAmount;

    void Start()
    {
        InstantiateUnits();
        formationHolder.GetComponent<FormationHolderController>().GeneratePoints();
        SelectionController.Instance.unitGroupList.Add(this.gameObject);
    }

    void InstantiateUnits()
    {
        for (int i = 0; i < unitAmount; i++)
        {
            GameObject instance = Instantiate(
                unitPrefab,
                formationHolder.transform.position, 
                Quaternion.identity, 
                transform
            );
        }
    }

    void OnDestroy() {
        if (Time.frameCount == 0) return;
        
        SelectionController.Instance.unitGroupList.Remove(this.gameObject);
    }
}
