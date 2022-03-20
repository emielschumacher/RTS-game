using Game.Components.Selections.Selectables.Contracts;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Game.Components.Selections.Selectables
{
    public class SelectableGroup : NetworkBehaviour, ISelectableGroup
    {
        public bool isSelected = false;
        public List<AbstractSelectable> selectableList = new List<AbstractSelectable>();

        public void SelectAll()
        {
            if (!hasAuthority) { return; }

            isSelected = true;

            foreach(AbstractSelectable selectable in selectableList) {
                selectable.HandleOnSelect();
            }
        }

        public void DeselectAll()
        {
            if (!hasAuthority) { return; }

            isSelected = false;

            foreach(AbstractSelectable selectable in selectableList) {
                selectable.HandleOnDeselect();
            }
        }
    }
}