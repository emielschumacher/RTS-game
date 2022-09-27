using UnityEngine;
using Game.Components.Formations.States.Contracts;
using Game.Components.Formations.Actions;
using Game.Components.Targets;

namespace Game.Components.Formations.States
{
    public class AttackState : IFormationState
    {
        private FormationUnitInFormationPositionAction _formationUnitInFormationPositionAction = new FormationUnitInFormationPositionAction();

        public IFormationState DoState(FormationBehaviour formationBehaviour)
        {
            FormationHolderBehaviour formationHolderBehaviour = formationBehaviour.GetFormationHolderBehaviour();

            foreach(FormationUnitBehaviour formationUnitBehaviour in formationBehaviour.GetFormationUnits()) {

                Targetable target = formationHolderBehaviour.GetTargeter().GetTarget();
                
                float distanceToTarget = Vector3.Distance(
                    target.transform.position,
                    formationUnitBehaviour.transform.position
                );

                if(distanceToTarget > 3f)
                {
                    _formationUnitInFormationPositionAction.DoAction(
                        formationUnitBehaviour,
                        formationHolderBehaviour
                    );

                    continue;
                }

                Debug.Log("ATTAAACKK!!!");
                //formationUnitBehaviour.formationUnitAnimationsBehaviour.isAttacking = true;
            }
            
            return this;
        }
    }
}