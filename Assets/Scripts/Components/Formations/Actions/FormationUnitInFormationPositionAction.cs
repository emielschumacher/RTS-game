using UnityEngine;
using System.Collections;
using Game.Components.Formations.States.Contracts;

namespace Game.Components.Formations.Actions
{
    public class FormationUnitInFormationPositionAction : MonoBehaviour
    {
        public void DoAction(
            FormationUnitBehaviour formationUnitBehaviour,
            FormationHolderBehaviour formationHolderBehaviour
        ) {
            //Vector3 desiredFormationPosition = formationHolderBehaviour.transform.TransformPoint(localStartPosition);
            Vector3 desiredFormationPosition = formationHolderBehaviour.transform.rotation
                * formationUnitBehaviour.localStartPosition
                + formationHolderBehaviour.transform.position
            ;

            if(
                !formationHolderBehaviour.GetNavigationBehaviour().isMoving
                && formationUnitBehaviour.transform.position == desiredFormationPosition
            ) {
                return;
            }

            formationUnitBehaviour
                .GetNavigationBehaviour()
                .SetDestination(desiredFormationPosition);
        }
    }
}