using UnityEngine;
using System.Collections.Generic;
using Game.Components.Formations;
using Game.Components.Formations.Contracts;

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