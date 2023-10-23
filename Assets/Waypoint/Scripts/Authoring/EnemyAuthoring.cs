using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Waypoint.Scripts.Component;

namespace Waypoint.Scripts.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float speed;
        public List<Transform> waypoints;
    }

    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            Speed speed = default;
            speed.value = authoring.speed;
            AddComponent(entity,speed);
            
            AddComponent<WaypointIndex>(entity);

            DynamicBuffer<Waypoints> waypointBuffer = AddBuffer<Waypoints>(entity);

            foreach (Transform wp in authoring.waypoints)
            {
                waypointBuffer.Add(new Waypoints(){value = wp.position});
            }
        }
    }
}