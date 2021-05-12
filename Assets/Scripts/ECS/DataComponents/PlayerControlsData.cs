using Unity.Entities;
using Unity.Mathematics;

namespace ECS.DataComponents
{
    [GenerateAuthoringComponent]
    public struct PlayerControlsData : IComponentData
    {
        public float3 Velocity;
        public float Movespeed;
    }
}