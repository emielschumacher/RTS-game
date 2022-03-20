using System.Collections.Generic;
using UnityEngine;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;

namespace Game.Components.Selections
{
    public class SelectionManager : MonoBehaviour, ISelectionManager
    {
        public List<AbstractSelectable> _selectedList = new List<AbstractSelectable>();

        public void DragSelect(AbstractSelectable selectable) {
            AddSelectable(selectable);
        }

        public void ClickSelect(AbstractSelectable selectable) {
            DeselectAll();
            AddSelectable(selectable);
        }

        public void ShiftClickSelect(AbstractSelectable selectable) {
            if(AddSelectable(selectable)) {
                return;
            }

            Deselect(selectable);
        }

        public bool AddSelectable(AbstractSelectable selectable)
        {
            if(_selectedList.Contains(selectable)) {
                return false;
            }

            SelectableGroup selectableGroup = selectable.GetSelectableGroup();

            if(selectableGroup != null) {
                selectableGroup.SelectAll();
                _selectedList.AddRange(selectableGroup.selectableList);

                return true;
            }

            _selectedList.Add(selectable);

            selectable.HandleOnSelect();

            return true;
        }

        public void DeselectAll() {
            _selectedList.ForEach(
                (AbstractSelectable selectable) => {
                    selectable.HandleOnDeselect();
                    selectable.GetSelectableGroup()?.DeselectAll();
                }
            );
            _selectedList.Clear();
        }

        public void Deselect(AbstractSelectable selectable)
        {
            SelectableGroup selectableGroup = selectable.GetSelectableGroup();

            if(selectableGroup != null) {
                selectableGroup.DeselectAll();

                foreach(AbstractSelectable selectableListItem in selectableGroup.selectableList) {
                    _selectedList.Remove(selectableListItem);
                }

                return;
            }
            
            selectable.HandleOnDeselect();
            _selectedList.Remove(selectable);
        }
    }
}