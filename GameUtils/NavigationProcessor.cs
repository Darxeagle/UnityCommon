using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Common.Scripts.GameUtils
{
    class NavigationProcessor
    {
        private NavMeshSurface _navMeshSurface;
        private NavMeshAgent _navMeshAgent;

        public NavigationProcessor(NavMeshSurface navMeshSurface)
        {
            _navMeshSurface = navMeshSurface;
            _navMeshAgent = new GameObject().AddComponent<NavMeshAgent>();
        }

        public Vector3 NormalizePosition(Vector3 position)
        {
            _navMeshAgent.transform.position = position;
            return _navMeshAgent.nextPosition;
        }

        public Vector3[] BuildPath(Vector3 position, Vector3 destination)
        {
            destination = NormalizePosition(destination);
            _navMeshAgent.transform.position = position;
            NavMeshPath path = new NavMeshPath();
            _navMeshAgent.CalculatePath(destination, path);
            return path.corners;
        }
    }
}
