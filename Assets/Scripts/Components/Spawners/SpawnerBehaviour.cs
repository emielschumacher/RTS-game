using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;
using Zenject;
using Game.Components.Formations;

namespace Game.Components.Spawners
{
    public class SpawnerBehaviour : NetworkBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform spawnPoint = null;
        [Inject] FormationBehaviour.Factory _formationFactory;

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
            FormationBehaviour instance = _formationFactory.Create();

            instance.transform.position = spawnPoint.position;
            instance.transform.rotation = spawnPoint.rotation;

            NetworkServer.Spawn(instance.transform.gameObject, connectionToClient);
        }

        public class Factory : PlaceholderFactory<SpawnerBehaviour> { }
    }
}