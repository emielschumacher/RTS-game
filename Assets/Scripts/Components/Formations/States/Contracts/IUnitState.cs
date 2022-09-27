using UnityEngine;
using System.Collections.Generic;
using Game.Components.Formations;


namespace Game.Components.Formations.States.Contracts
{
    public interface IUnitState
    {
        public IUnitState DoState(FormationUnitBehaviour unit);
    }
}