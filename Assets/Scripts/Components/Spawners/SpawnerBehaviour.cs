using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

namespace Game.Components.Spawners
{
    public class SpawnerBehaviour : NetworkBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform spawnPoint = null;
        [SerializeField] private GameObject _formationPrefab;

        [Client]
        public void OnPointerClick(PointerEventData eventData)
        {
            // if(eventData.button != PointerEventData.InputButton.Left) return;

            if(!hasAuthority) return;

            CmdSpawnUnit();
        }

        [Command]
        private void CmdSpawnUnit()
        {
            GameObject instance = Instantiate(_formationPrefab);

            instance.transform.position = spawnPoint.position;
            instance.transform.rotation = spawnPoint.rotation;

            NetworkServer.Spawn(instance.transform.gameObject, connectionToClient);
        }
    }
}