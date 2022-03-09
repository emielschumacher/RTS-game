using UnityEngine;
using Mirror;
using Game.Components.Spawners;
using Game.Components.Networking;

namespace Game.Components.Networking
{
    public class MyNetworkManager : NetworkManager
    {
        [SerializeField] private GameObject _myNetworkPlayerPrefab;
        [SerializeField] private GameObject _SpawnerPrefab;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);

            // Player
            Transform startPos = GetStartPosition();
            GameObject playerInstance = Instantiate(
                _myNetworkPlayerPrefab,
                startPos.position,
                startPos.rotation
            );

            NetworkServer.Spawn(playerInstance.transform.gameObject, conn);
            playerInstance.name = $"NetworkPlayer [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, playerInstance.transform.gameObject);

            // Spawner
            GameObject spawnerInstance = Instantiate(
                _SpawnerPrefab,
                conn.identity.transform.position,
                conn.identity.transform.rotation
            );

            NetworkServer.Spawn(spawnerInstance.transform.gameObject, conn);
        }
    }
}