using UnityEngine;

namespace Game.Components.Formations
{
    public class FormationHolderPointBehaviour : MonoBehaviour
    {
        public Vector3 formationOffset = Vector3.zero;
        public FormationHolderBehaviour formationHolder;


        private void Update()
        {
            Debug.Log(formationHolder);
            transform.position = formationHolder.transform.position + formationOffset;
        }
    }
}