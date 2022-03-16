using UnityEngine;
using System.Collections.Generic;

namespace Game.Components.Formations.Contracts
{
    public interface IFormation
    {
        public List<Vector3> GridFormation(
            int unitAmount,
            Vector3 targetPosition
        );
    }
}