using UnityEngine.AI;

namespace Game.Components.Navigations.Contracts
{  
    public interface INavigationPathPending
    {
        bool IsPathPending(
            NavMeshAgent navMeshAgent
        );
    }
}