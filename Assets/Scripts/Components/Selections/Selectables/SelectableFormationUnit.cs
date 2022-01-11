using UnityEngine;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables.Contracts;
using Game.Components.Navigations;
using Game.Components.Navigations.Contracts;
using Game.Components.Formations;
using Game.Components.Formations.Contracts;
using System.Collections.Generic;
using Zenject;

namespace Game.Components.Selections.Selectables
{
    public class SelectableFormationUnit : AbstractSelectable, ISelectableFormationUnit
    {
    }
}