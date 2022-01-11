using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public List<GameObject> unitGroupList = new List<GameObject>();
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> selectedUnits = new List<GameObject>();
    public List<GameObject> selectedUnitGroups = new List<GameObject>();
    private static SelectionController _instance;
    public static SelectionController Instance { get{ return _instance; } }
    
    void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject objTodadd) {
        DeselectAll();
        SelectUnitOrUnitGroup(objTodadd);
    }

    public void DragSelect(GameObject objTodadd) {
        SelectUnitOrUnitGroup(objTodadd);
    }

    public void ShiftClickSelect(GameObject objTodadd) {
        if(SelectUnitOrUnitGroup(objTodadd)) {
            return;
        }

        Deselect(objTodadd);
    }

    private bool SelectUnitOrUnitGroup(GameObject objTodadd)
    {
        if(SelectUnitGroup(objTodadd)) {
            return true;
        }

        if(SelectUnit(objTodadd)) {
            return true;
        }

        return false;
    }

    private bool SelectUnit(GameObject unitTodadd)
    {
        if(unitTodadd.tag != Tags.unitTag) {
            return false;
        }

        if(selectedUnits.Contains(unitTodadd)) {
            return false;
        }

        selectedUnits.Add(unitTodadd);
        unitTodadd.GetComponent<UnitController>().selectIndicator.SetActive(true);

        return true;
    }

    private GameObject GetUnitGroupByUnit(GameObject unit)
    {
        GameObject unitGroup = unit.GetComponent<UnitController>().unitGroup;

        if(unitGroup.tag != Tags.unitGroupTag) {
            return null;
        }

        return unitGroup;
    }

    private bool SelectUnitGroup(GameObject unitTodadd)
    {
        GameObject unitGroup = unitTodadd.GetComponent<UnitController>().unitGroup;

        if (unitGroup.tag != Tags.unitGroupTag) {
            return false;
        }

        if(!selectedUnitGroups.Contains(unitGroup)) {
            selectedUnitGroups.Add(unitGroup);
        }

        foreach(GameObject unit in unitGroup.GetComponent<UnitGroupController>().unitList)
        {
            SelectUnit(unit);
        }

        return true;
    }

    public void DeselectAll() {
        foreach(var unit in selectedUnits) {
            unit.GetComponent<UnitController>().selectIndicator.SetActive(false);
        }

        selectedUnits.Clear();
        selectedUnitGroups.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {
        unitToDeselect.GetComponent<UnitController>().selectIndicator.SetActive(false);
        selectedUnits.Remove(unitToDeselect);

        GameObject unitGroup = GetUnitGroupByUnit(unitToDeselect);

        if(unitGroup) {
            selectedUnitGroups.Remove(unitGroup);
        }
    }
}
