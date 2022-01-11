using UnityEngine;

namespace Game.Components.Movements
{
    public class MovementDetection : Contracts.IMovementDetection
    {
        public Vector3 lastPosition;
        private bool _IsMoving;

        public bool IsMoving(
            Vector3 currentPosition
        ) {
            // _IsMoving = Mathf.Approximately(currentPosition, lastPosition);

            lastPosition = currentPosition;

            return _IsMoving;
        }
    }
}