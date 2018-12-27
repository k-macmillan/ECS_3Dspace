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
        public struct ProjectileComponents
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Position> Position;
            public ComponentDataArray<VelocityVector> Velocity;
            [ReadOnly] public ComponentDataArray<PlayerFaction> Collider;
        }

        [Inject] private ProjectileComponents m_Projectiles;

        public struct ShipsComponents
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Position> Position;
            public ComponentDataArray<HealthUpdate> Health;
            public ComponentDataArray<VelocityVector> Velocity;
        }

        [Inject] private ShipsComponents m_Ships;

        private static readonly float radius = (Settings.ProjectileRadius + Settings.SubShipRadius) * (Settings.ProjectileRadius + Settings.SubShipRadius);


        protected override void OnUpdate()
        {
            for (int i = 0; i < m_Projectiles.Length; ++i)
            {
                for (int j = 0; j < m_Ships.Length; ++j)
                {
                    // Ensure it's not the player ship!
                    // Better to not include the player ship in this grouping...
                    if (!Common.SameFloat3(m_Ships.Position[j].Value, PlayerController.em.GetComponentData<Position>(PlayerController.ship).Value))
                    {
                        if (Common.DistanceSquared(m_Projectiles.Position[i].Value, m_Ships.Position[j].Value) <= radius)
                        {
                            Common.markedForDelete.Enqueue(m_Projectiles.Position[i].Value);
                            // Is this necessary? Negligible computation
                            m_Projectiles.Velocity[i] = new VelocityVector { Value = new float3(0f, 0f, 0f) };

                            int health = m_Ships.Health[j].Health - Settings.ProjectileDamage;
                            if (health > 0)
                            {
                                HealthUpdate healthUpdate = new HealthUpdate { Health = health, Faction = m_Ships.Health[j].Faction };
                                m_Ships.Health[j] = healthUpdate;
                            }
                            else
                            {
                                Common.markedForDelete.Enqueue(m_Ships.Position[j].Value);
                                m_Ships.Velocity[j] = new VelocityVector { Value = new float3(0f, 0f, 0f) };
                            }
                        }
                    }
                }
                // Ensure projectiles that are far away are removed
                if (Common.DistanceSquared(m_Projectiles.Position[i].Value, Common.zero) > 900000f)
                {
                    Common.markedForDelete.Enqueue(m_Projectiles.Position[i].Value);
                    // Is this necessary? Negligible computation
                    m_Projectiles.Velocity[i] = new VelocityVector { Value = new float3(0f, 0f, 0f) };
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
                }
            }
        }
    }
}