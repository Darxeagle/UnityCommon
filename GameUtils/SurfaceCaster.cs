using System;
using UnityEngine;

namespace Assets.Common.Scripts.GameUtils
{
    class SurfaceCaster : IPlaneCaster
    {
        private const float RAY_HEIGHT = 100f;
        private const float RAY_LENGTH = 150f;

        private Mesh _mesh;
        private Transform _transform;
        private LayerMask _layerMask = new LayerMask();

        public SurfaceCaster(GameObject gameObject)
        {
            if (gameObject.GetComponent<MeshFilter>() == null) throw new Exception("SurfaceCaster: this objects has no MeshFilter component");
            _mesh = gameObject.GetComponent<MeshFilter>().mesh;
            gameObject.layer = _layerMask;

            _transform = gameObject.transform;
        }

        public Vector2 GetPlanePosition(Vector3 surfacePosition)
        {
            return new Vector2(surfacePosition.x / _mesh.bounds.size.x + 0.5f, surfacePosition.z / _mesh.bounds.size.z + 0.5f);
        }

        public Vector3 GetSurfacePosition(Vector2 planePosition)
        {
            var rayPosition = _transform.TransformPoint(
                new Vector3((planePosition.x - 0.5f)*_mesh.bounds.size.x, RAY_HEIGHT,
                    (planePosition.y - 0.5f)*_mesh.bounds.size.z));
            var ray = new Ray(rayPosition, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, RAY_LENGTH, _layerMask))
            {
                var surfacePosition = _transform.InverseTransformPoint(hit.point);
                return surfacePosition;
            }
            else
            {
                return new Vector3((planePosition.x - 0.5f) * _mesh.bounds.size.x, 0f, (planePosition.y - 0.5f) * _mesh.bounds.size.z);
            }
        }
    }
}
