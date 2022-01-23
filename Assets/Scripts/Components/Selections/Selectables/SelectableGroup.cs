using UnityEngine;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Selections.Selectables.Contracts;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Formations;
using Game.Components.Formations.Contracts;
using System.Collections.Generic;
using Zenject;
using Mirror;

namespace Game.Components.Selections.Selectables
{
    public class SelectableGroup : NetworkBehaviour, ISelectableGroup
    {
        public bool isSelected = false;
        public List<AbstractSelectable> selectableList = new List<AbstractSelectable>();

        [Command]
        public void SelectAll()
        {
            isSelected = true;

            foreach(AbstractSelectable selectable in selectableList) {
                selectable.HandleOnSelect();
            }
        }

        [Command]
        public void DeselectAll()
        {
            isSelected = false;

            foreach(AbstractSelectable selectable in selectableList) {
                selectable.HandleOnDeselect();
            }
        }
    }
}