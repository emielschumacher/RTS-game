using Game.Components.Selections.Selectables.Contracts;
using System.Collections.Generic;
using Mirror;

namespace Game.Components.Selections.Selectables
{
    public class SelectableGroup : NetworkBehaviour, ISelectableGroup
    {
        public bool isSelected = false;
        public List<ISelectable> selectableList = new List<ISelectable>();

        public void SelectAll()
        {
            if (!hasAuthority) { return; }

            isSelected = true;

            foreach(ISelectable selectable in selectableList) {
                selectable.HandleOnSelect();
            }
        }

        public void DeselectAll()
        {
            if (!hasAuthority) { return; }

            isSelected = false;

            foreach(ISelectable selectable in selectableList) {
                selectable.HandleOnDeselect();
            }
        }
    }
}