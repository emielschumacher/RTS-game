using UnityEngine;
using System.Collections;
using Game.Components.Formations;
using Game.Components.Formations.States.Contracts;

namespace Game.Components.Formations.States
{
    public class HoldFormationPositionState : IUnitState
    {
        public IUnitState DoState(FormationUnitBehaviour unit)
        {
            FormationHolderBehaviour formationHolderBehaviour = unit.formationHolderBehaviour;

            //Vector3 desiredFormationPosition = formationHolderBehaviour.transform.TransformPoint(localStartPosition);
            Vector3 desiredFormationPosition = formationHolderBehaviour.transform.rotation
                * unit.localStartPosition
                + formationHolderBehaviour.transform.position
            ;

            if(
                !formationHolderBehaviour.GetNavigationBehaviour().isMoving
                && unit.transform.position == desiredFormationPosition
            ) {
                return unit.holdFormationPositionState;
            }

            unit.GetNavigationBehaviour().SetDestination(desiredFormationPosition);
            
            return unit.holdFormationPositionState;
        }
    }
}