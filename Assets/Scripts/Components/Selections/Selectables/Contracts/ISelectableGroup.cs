using UnityEngine;
using Game.Components.Navigations;
using Game.Components.Formations;

namespace Game.Components.Selections.Selectables.Contracts
{
    public interface ISelectableGroup
    {
        void SelectAll();
        void DeselectAll();
    }
}