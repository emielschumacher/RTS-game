using UnityEngine;
using Mirror;
using System.Collections.Generic;

namespace Game.Components.Networking
{
    public class ClientOnlyBehaviour : NetworkBehaviour
    {
        public List<GameObject> gameObjects = new List<GameObject>();

        public void Start()
        {
            if(isServer && !isClient) {
                foreach(GameObject gameObject in gameObjects) {
                    gameObject.SetActive(false);
                }
            }
        }

        public override void OnStartClient()
        {
            foreach(GameObject gameObject in gameObjects) {
                 gameObject.SetActive(true);
            }
        }
    }
}