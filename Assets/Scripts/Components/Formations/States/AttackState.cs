using UnityEngine;
using System.Collections;
using Game.Components.Formations;
using Game.Components.Formations.States.Contracts;

namespace Game.Components.Formations.States
{
    public class AttackState : IUnitState
    {
        public IUnitState DoState(FormationUnitBehaviour unit)
        {
            Debug.Log("Attacking!");

            //if(onEnemyDiedEvent)
            //{
            //    return holdFormationPositionState;
            //}

            return unit.attackState;
        }
    }
}