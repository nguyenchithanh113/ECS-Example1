using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Waypoint.Scripts.Component;
using NotImplementedException = System.NotImplementedException;

namespace Waypoint.Scripts.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float speed;
        public List<Transform> waypoints;
    }

    [TemporaryBakingType]
    public struct TagComponent : IComponentData
    {
        
    }
    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            Speed speed = default;
            speed.value = authoring.speed;
            AddComponent(entity,speed);
            
            AddComponent<TagComponent>(entity);
            
            AddComponent<WaypointIndex>(entity);

            DynamicBuffer<Waypoints> waypointBuffer = AddBuffer<Waypoints>(entity);

            foreach (Transform wp in authoring.waypoints)
            {
                waypointBuffer.Add(new Waypoints(){value = wp.position});
            }
        }
    }
    
    [WorldSystemFilter(WorldSystemFilterFlags.BakingSystem)]
    public partial class BakingSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<TagComponent>().ForEach((in Entity entity,in Speed speed, in DynamicBuffer<Waypoints> path) =>
            {
                Debug.Log($"Entity {entity.Index} Speed: {speed.value} Path count {path.Length}");
            }).Schedule();
        }
    }
}