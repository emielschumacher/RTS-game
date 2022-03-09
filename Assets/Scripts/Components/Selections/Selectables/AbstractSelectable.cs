using UnityEngine;
using UnityEngine.Events;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables.Contracts;
using Mirror;

namespace Game.Components.Selections.Selectables
{
    public abstract class AbstractSelectable : NetworkBehaviour, ISelectable
    {
        public SelectableGroup _selectableGroup;
        public UnityEvent onSelectEvent;
        public UnityEvent onDeselectEvent;
        bool isSelected = false;

        [Command]
        public void HandleOnSelect()
        {
            isSelected = true;
            onSelectEvent?.Invoke();
        }

        [Command]
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
            // selectableGroup.selectableList.Add(this);
            
            _selectableGroup = selectableGroup;
        }
    }
}