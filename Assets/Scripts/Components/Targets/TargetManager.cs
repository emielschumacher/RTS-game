using UnityEngine;
using Game.Components.Selections;
using Game.Components.Selections.Selectables;
using Game.Components.Formations;
using UnityEngine.Events;

namespace Game.Components.Targets
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager instance { get; private set; }

        private void Start()
        {
            if (instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
        
        public void TrySetTarget(Targetable target)
        {
            if(!SelectionManager.instance.HasSelectedObjects())
            {
                return;
            }

            foreach(AbstractSelectable selectedFormationUnit in SelectionManager.instance.selectedFormationUnits)
            {
                selectedFormationUnit
                    .GetComponent<FormationUnitBehaviour>()
                    .formationHolderBehaviour
                    .GetTargeter()
                    .CmdSetTarget(target.gameObject)
                ;
            }
        }
    }
}