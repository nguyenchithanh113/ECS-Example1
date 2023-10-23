using Unity.Entities;
using UnityEngine.Serialization;

namespace Waypoint.Scripts.Component
{
    public struct WaypointIndex : IComponentData
    {
        public int value;
    }
}