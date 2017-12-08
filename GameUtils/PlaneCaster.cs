using System;
using UnityEngine;

namespace Assets.Common.Scripts.GameUtils
{
    class PlaneCaster : IPlaneCaster
    {
        private Mesh _mesh;

        public PlaneCaster(GameObject gameObject)
        {
            if (gameObject.GetComponent<MeshFilter>() == null) throw new Exception("PlaneCaster: this objects has no MeshFilter component");
            _mesh = gameObject.GetComponent<MeshFilter>().mesh;
        }

        public Vector2 GetPlanePosition(Vector3 surfacePosition)
        {
            return new Vector2(surfacePosition.x / _mesh.bounds.size.x + 0.5f, surfacePosition.z / _mesh.bounds.size.z + 0.5f);
        }

        public Vector3 GetSurfacePosition(Vector2 planePosition)
        {
            return new Vector3((planePosition.x - 0.5f) * _mesh.bounds.size.x, 0f, (planePosition.y - 0.5f) * _mesh.bounds.size.z);
        }
    }
}
