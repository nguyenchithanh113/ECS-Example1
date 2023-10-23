using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;
using Waypoint.Scripts.Component;

namespace Waypoint.Scripts.System
{
    public partial class MoveSystemBase : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = World.Time.DeltaTime;
            Entities.WithAll<RenderBounds>().ForEach((
                ref LocalTransform transform, 
                ref WaypointIndex index,
                in DynamicBuffer<Waypoints> waypoints,
                in Speed speed) =>
            {
                var target = waypoints[index.value].value;
                var dir = math.normalizesafe((target - transform.Position));
                transform.Position += dir * speed.value * deltaTime;

                if (math.distance(target, transform.Position) < 0.1f)
                {
                    index.value = (index.value + 1) % waypoints.Length;
                }
            }).Schedule();
        }
    }
}