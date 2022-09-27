using UnityEngine;
using Game.Components.Formations.States.Contracts;
using Game.Components.Formations.Actions;

namespace Game.Components.Formations.States
{
    public class PadrolState : IFormationState
    {
        private FormationUnitInFormationPositionAction _formationUnitInFormationPositionAction = new FormationUnitInFormationPositionAction();

        public IFormationState DoState(FormationBehaviour formationBehaviour)
        {
            FormationHolderBehaviour formationHolderBehaviour = formationBehaviour.GetFormationHolderBehaviour();

            foreach(FormationUnitBehaviour formationUnitBehaviour in formationBehaviour.GetFormationUnits()) {

                _formationUnitInFormationPositionAction.DoAction(
                    formationUnitBehaviour,
                    formationHolderBehaviour
                );
            }
            
            return this;
        }
    }
}