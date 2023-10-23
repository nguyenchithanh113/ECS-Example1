using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace Waypoint.Scripts.Component
{
    public struct Waypoints : IBufferElementData
    {
        public float3 value;
    }
}