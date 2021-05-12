using ECS.DataComponents;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECS.Systems
{
    public class PlayerInputSystem : SystemBase
    {
        private float3 vel;
        private float moveSpeed;

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            var q = GetEntityQuery(ComponentType.ReadWrite<PlayerControlsData>());
            var data = EntityManager.GetComponentData<PlayerControlsData>(q.GetSingletonEntity());
            vel = data.Velocity;
            moveSpeed = data.Movespeed;
        }

        protected override void OnUpdate()
        {
            float tc = Time.DeltaTime;
            Entities
                .WithoutBurst()
                .ForEach((Entity ent, ref PlayerControlsData controls, ref Translation trans) =>
                {
                    trans.Value.x += (vel.x * moveSpeed * tc);
                    trans.Value.y += (vel.y * moveSpeed * tc);
                    trans.Value.z += (vel.z * moveSpeed * tc);
                }).Run();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                vel.x = context.ReadValue<Vector2>().x;
                vel.z = context.ReadValue<Vector2>().y;
            }
            else if (context.canceled)
            {
                vel.x = 0f;
                vel.z = 0f;
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                moveSpeed *= 10f;
            }
            else if (context.canceled)
            {
                moveSpeed = 10f;
            }
        }
    }
}