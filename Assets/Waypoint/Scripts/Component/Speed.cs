using Unity.Entities;
using UnityEngine.Serialization;

namespace Waypoint.Scripts.Component
{
    public struct Speed : IComponentData
    {
        public float value;
    }
}