using System.Collections.Generic;
using UnityEngine;
using Game.Components.Formations.Contracts;

namespace Game.Components.Formations
{
    public class Formation : MonoBehaviour, IFormation
    {
        public List<Vector3> GridFormation(
            int unitAmount,
            Vector3 targetPosition
        ) {
            List<Vector3> positionList = new List<Vector3>();
            
            int unitColumns = Mathf.CeilToInt(unitAmount / 2);
            float xSize = 1;
            float zSize = 1;
            float xSpace = 1 + xSize;
            float zSpace = 2 + zSize;
            float xCenter = ((unitAmount * xSpace / 2) - 2) / 2;
            
            for (int i = 0; i < unitAmount; i++)
            {
                Vector3 formationPosition = Vector3.zero;

                if(unitAmount > 1) {
                    formationPosition = new Vector3(
                        targetPosition.x - xCenter + (xSpace * (i % unitColumns)),
                        0,
                        targetPosition.z + 1.5f + (-zSpace * (i / unitColumns))
                    );
                } else {
                    formationPosition = new Vector3(
                        targetPosition.x, 
                        0, 
                        targetPosition.z + 0.5f
                    );
                }

                positionList.Add(formationPosition);
            }

            return positionList;
        }
    }
}