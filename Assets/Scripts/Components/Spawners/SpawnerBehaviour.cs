using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

namespace Game.Components.Spawners
{
    public class SpawnerBehaviour : NetworkBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform spawnPoint = null;
        [SerializeField] private GameObject _formationPrefab;

        public void Awake() {
            GameObject cameraRigBase = Camera.main.transform.parent.gameObject;

            cameraRigBase.transform.position = transform.position;

            cameraRigBase.transform.position = new Vector3(
                transform.position.x,
                cameraRigBase.transform.position.y,
                transform.position.z
            );

            Invoke("EnableCamera", 0.2f);
        }

        void EnableCamera() {
            GameObject cameraRigBase = Camera.main.transform.parent.gameObject;
            cameraRigBase.GetComponent<CameraBehaviour>().enabled = true;
        }

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