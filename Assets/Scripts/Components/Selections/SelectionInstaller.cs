using UnityEngine;
using Zenject;
using Game.Components.Selections;
using Game.Components.Selections.Contracts;
using Game.Components.Selections.Selectables;
using Game.Components.Selections.Selectables.Contracts;

namespace Game.Components.Selections
{
    public class SelectionInstaller : Installer<SelectionInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ISelectionManager>()
                .To<SelectionManager>()
                .AsSingle();
            Container
                .Bind<ISelectableFormationUnit>()
                .To<SelectableFormationUnit>()
                .AsSingle();
            // Container
            //     .Bind<ISelectableBuidling>()
            //     .To<SelectableBuilding>()
            //     .AsSingle();
            // ;
        }
    }
}