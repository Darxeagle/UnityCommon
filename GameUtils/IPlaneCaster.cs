using UnityEngine;

namespace Assets.Common.Scripts.GameUtils
{
    public interface IPlaneCaster
    {
        Vector2 GetPlanePosition(Vector3 surfacePosition);
        Vector3 GetSurfacePosition(Vector2 planePosition);
    }
}
