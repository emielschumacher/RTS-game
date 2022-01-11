using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Components.Selections.Selectables.Contracts;
using Game.Components.Selections.Contracts;
using Game.Components.Selections;
using Game.Components.Selections.Selectables;

namespace Game.Components.Selections.Contracts
{
    public interface ISelectionManager
    {
        void DragSelect(AbstractSelectable selectable);
        void ClickSelect(AbstractSelectable selectable);
        void ShiftClickSelect(AbstractSelectable selectable);
        void DeselectAll();
    }
}