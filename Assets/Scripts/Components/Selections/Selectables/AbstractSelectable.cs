using UnityEngine;
using UnityEngine.Events;
using Game.Components.Selections.Selectables.Contracts;
using Mirror;

namespace Game.Components.Selections.Selectables
{
    public abstract class AbstractSelectable : NetworkBehaviour, ISelectable
    {
        public UnityEvent onSelectEvent;
        public UnityEvent onDeselectEvent;
        SelectableGroup _selectableGroup;
        bool _isSelected = false;

        public void HandleOnSelect()
        {
            if (!hasAuthority) { return; }

            _isSelected = true;
            onSelectEvent?.Invoke();
        }

        public void HandleOnDeselect()
        {
            if (!hasAuthority) { return; }

            _isSelected = false;
            onDeselectEvent?.Invoke();
        }

        public SelectableGroup GetSelectableGroup()
        {
            return _selectableGroup;
        }

        public void SetSelectableGroup(SelectableGroup selectableGroup)
        {
            if (!hasAuthority) { return; }

            selectableGroup.selectableList.Add(this);
            
            _selectableGroup = selectableGroup;
        }
    }
}