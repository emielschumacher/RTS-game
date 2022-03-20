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
        private NetworkConnection _conn;
        public MyNetworkPlayer myNetworkPlayer { get; private set; }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            _conn = conn;

            base.OnServerAddPlayer(conn);
            //SpawnNetworkPlayer();

            // Spawner
            GameObject spawnerInstance = Instantiate(
                _SpawnerPrefab,
                conn.identity.transform.position,
                conn.identity.transform.rotation
            );

            NetworkServer.Spawn(spawnerInstance.transform.gameObject, _conn);
        }

        //private void SpawnNetworkPlayer()
        //{
        //    Transform startPos = GetStartPosition();
        //    GameObject playerInstance = Instantiate(
        //        _myNetworkPlayerPrefab,
        //        startPos.position,
        //        startPos.rotation
        //    );
        //    myNetworkPlayer = playerInstance.GetComponent<MyNetworkPlayer>();

        //    NetworkServer.Spawn(playerInstance.transform.gameObject, _conn);
        //    playerInstance.name = $"NetworkPlayer [connId={_conn.connectionId}]";
        //    NetworkServer.AddPlayerForConnection(_conn, playerInstance.transform.gameObject);
        //}
    }
}