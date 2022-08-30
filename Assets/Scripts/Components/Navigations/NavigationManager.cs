using UnityEngine;

namespace Game.Components.Navigations
{
    public class NavigationManager : MonoBehaviour
    {
        public static NavigationManager instance { get; private set; }

        private void Start()
        {
            if (instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
    }
}