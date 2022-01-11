using UnityEngine;
using Zenject;
using Mirror;

namespace Game.Components.Formations
{
    public class FormationHolderPointBehaviour : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<FormationHolderPointBehaviour> { }

    }
}