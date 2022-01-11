using UnityEngine;
using Mirror;
using Zenject;
using Game.Components.Spawners;
using Game.Components.Networking;

namespace Game.Components.Networking
{
    public class MyNetworkManager : NetworkManager
    {
        [Inject] SpawnerBehaviour.Factory _spawnerFactory;
        [Inject] MyNetworkPlayer.Factory _myNetworkPlayer;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // base.OnServerAddPlayer(conn);

            Transform startPos = GetStartPosition();

            MyNetworkPlayer playerInstance = _myNetworkPlayer.Create();

            NetworkServer.Spawn(playerInstance.transform.gameObject, conn);

            Debug.Log(playerInstance.transform.gameObject.name);

            playerInstance.transform.position = startPos.position;
            playerInstance.transform.rotation = startPos.rotation;
            playerInstance.name = $"NetworkPlayer [connId={conn.connectionId}]";

            NetworkServer.AddPlayerForConnection(conn, playerInstance.transform.gameObject);

            // Spawner
            SpawnerBehaviour spawnerInstance = _spawnerFactory.Create();

            spawnerInstance.transform.position = conn.identity.transform.position;
            spawnerInstance.transform.rotation = conn.identity.transform.rotation;

            NetworkServer.Spawn(spawnerInstance.transform.gameObject, conn);
        }
    }
}