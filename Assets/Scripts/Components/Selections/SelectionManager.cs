using UnityEngine;
using System.Collections.Generic;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Selections.Selectables.Contracts;
using System;

namespace Game.Components.Selections
{
    public class SelectionManager : MonoBehaviour, ISelectionManager
    {
        public static SelectionManager instance { get; private set; }

        public List<ISelectable> selectedBuildings = new List<ISelectable>();
        public List<ISelectable> selectedFormationUnits = new List<ISelectable>();

        private void Start()
        {
            if (instance != null && instance != this)  {
                Destroy(this);
            } else {
                instance = this;
            }
        }

        public bool HasSelectedObjects()
        {
            return (
                selectedBuildings.Count > 0
                || selectedFormationUnits.Count > 0
            );
        }

        private List<ISelectable> GetSelectedListForType(
            ISelectable selectable
        ) {
            if(selectable is ISelectableFormationUnit)
            {
                return selectedFormationUnits;
            }
            
            if(selectable is ISelectableBuidling)
            {
                return selectedBuildings;
            }

            throw new Exception("No list found for given type");
        }

        public void DragSelect(AbstractSelectable selectable) {
            AddSelectable(selectable);
        }

        public void ClickSelect(AbstractSelectable selectable) {
            DeselectAll();
            AddSelectable(selectable);
        }

        public void ShiftClickSelect(AbstractSelectable selectable) {
            if(AddSelectable(selectable))
            {
                return;
            }

            Deselect(selectable);
        }

        public bool AddSelectable(AbstractSelectable selectable)
        {
            List<ISelectable> selectedList = GetSelectedListForType(selectable);

            if(selectedList.Contains(selectable)) {
                return false;
            }

            SelectableGroup selectableGroup = selectable.GetSelectableGroup();

            if(selectableGroup != null) {
                selectableGroup.SelectAll();
                Debug.Log("SelectableGroup select all!");

                selectedList.AddRange(selectableGroup.selectableList);

                return false;
            }

            selectedList.Add(selectable);
            selectable.HandleOnSelect();
            return true;
        }

        public void DeselectAll() {
            HandleDeselectAll(selectedFormationUnits);
            HandleDeselectAll(selectedBuildings);
        }

        private void HandleDeselectAll(List<ISelectable> selectedList) {
            selectedList.ForEach(
                (ISelectable selectable) => {
                    selectable.HandleOnDeselect();
                    selectable.GetSelectableGroup()?.DeselectAll();
                }
            );
            selectedList.Clear();
        }

        public void Deselect(AbstractSelectable selectable)
        {
            if(!selectable.netIdentity.hasAuthority)
            {
                return;
            }

            List<ISelectable> selectedList = GetSelectedListForType(selectable);
            SelectableGroup selectableGroup = selectable.GetSelectableGroup();

            if(selectableGroup != null) {
                selectableGroup.DeselectAll();

                foreach(ISelectable selectableListItem in selectableGroup.selectableList) {
                    selectedList.Remove(selectableListItem);
                }

                return;
            }
            
            selectable.HandleOnDeselect();
            selectedList.Remove(selectable);
        }
    }
}