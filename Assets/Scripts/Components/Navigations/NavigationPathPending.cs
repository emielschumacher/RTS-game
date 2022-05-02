using UnityEngine;
using UnityEngine.AI;
using Game.Components.Navigations.Contracts;

namespace Game.Components.Navigations
{
    public class NavigationPathPending : INavigationPathPending
    {
        public bool IsPathPending(
            NavMeshAgent navMeshAgent
        ) {
            return !(
                    !navMeshAgent.pathPending
                    && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
                    && !navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f
                ) || navMeshAgent.nextPosition == Vector3.zero
            ;
            // (_navMeshAgent.nextPosition - transform.position) == Vector3.zero
        }
    }
}