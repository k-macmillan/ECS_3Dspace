// NOTE: Lets raycast the velocity vector and only check distance if there is an impact.

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{
    /// <summary>
    /// Test projectiles colliding with other projectiles. This is to test a rudimentary collider.
    /// </summary>
    public class ProjectileColliderSystem : ComponentSystem
    {
        public struct Data
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Position> Position;
            public ComponentDataArray<VelocityVector> Velocity;
            [ReadOnly] public ComponentDataArray<PlayerFaction> Collider;
        }

        [Inject] private Data m_Data;

        private static readonly float radiusSqr = Settings.ProjectileRadius * Settings.ProjectileRadius;


        protected override void OnUpdate()
        {
            float dt = Time.deltaTime;
            
            for (int i = 0; i < m_Data.Length; ++i)
            {
                for (int j = 0; j < m_Data.Length; ++j)
                {
                    if (Common.DistanceSquared(m_Data.Position[i].Value, m_Data.Position[j].Value) < radiusSqr)
                    {
                        if (i != j)
                        {
                            Debug.Log("IMPACT DETECTED!");
                            Common.markedForDelete.Enqueue(m_Data.Position[i].Value);
                            // Is this necessary? Negligible computation
                            m_Data.Velocity[i] = new VelocityVector { Value = new float3(0f, 0f, 0f) };
                        }
                    }
                }
            }       
            while (Common.markedForDelete.Count > 0)
            {
                ProjectileHandler.ClearProjectile(Common.markedForDelete.Dequeue());
            }
            
        }
    }

    public static class ProjectileHandler
    {
        public static void ClearProjectile(float3 position)
        {
            EntityManager em = World.Active.GetExistingManager<EntityManager>();
            var entities = em.GetAllEntities();
            for (int i = 0; i < entities.Length; ++i)
            {
                if (Common.SameFloat3(em.GetComponentData<Position>(entities[i]).Value, position))
                {
                    em.DestroyEntity(entities[i]);
                    Debug.Log("PROJECTILE CLEARED!");
                }
            }
        }
        
    }
}