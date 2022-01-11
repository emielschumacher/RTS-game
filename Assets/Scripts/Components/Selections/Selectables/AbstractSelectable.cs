using UnityEngine;
using UnityEngine.Events;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables.Contracts;
using Zenject;

namespace Game.Components.Selections.Selectables
{
    public abstract class AbstractSelectable : MonoBehaviour, ISelectable
    {
        public SelectableGroup _selectableGroup;
        public UnityEvent onSelectEvent;
        public UnityEvent onDeselectEvent;
        bool isSelected = false;

        public void HandleOnSelect()
        {
            isSelected = true;
            onSelectEvent?.Invoke();
        }

        public void HandleOnDeselect()
        {
            isSelected = false;
            onDeselectEvent?.Invoke();
        }

        public SelectableGroup GetSelectableGroup()
        {
            return _selectableGroup;
        }

        public void SetSelectableGroup(SelectableGroup selectableGroup) {
            selectableGroup.selectableList.Add(this);
            
            _selectableGroup = selectableGroup;
        }
    }
}