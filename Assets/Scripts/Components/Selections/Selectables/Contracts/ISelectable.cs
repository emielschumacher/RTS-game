namespace Game.Components.Selections.Selectables.Contracts
{
    public interface ISelectable
    {
        SelectableGroup GetSelectableGroup();
        void HandleOnSelect();
        void HandleOnDeselect();
        void SetSelectableGroup(SelectableGroup selectableGroup);
    }
}