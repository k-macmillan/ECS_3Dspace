using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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
            [ReadOnly] public ComponentDataArray<VelocityVector> Velocity;
            [ReadOnly] public ComponentDataArray<PlayerFaction> Collider;
        }

        [Inject] private ProjectileComponents m_Projectiles;

        public struct ShipsComponents
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Position> Position;
            public ComponentDataArray<Health> Health;
            public ComponentDataArray<VelocityVector> Velocity;
        }

        [Inject] private ShipsComponents m_Ships;

        private static readonly float radius = (Settings.ProjectileRadius + Settings.SubShipRadius) * (Settings.ProjectileRadius + Settings.SubShipRadius);
        private static bool marked = false;



        /// <summary>
        /// Checks for projectile collisions with ComponentSystem's OnUpdate
        /// </summary>
        protected override void OnUpdate()
        {
            for (int i = 0; i < m_Projectiles.Length; ++i)
            {
                // Ensure projectiles that are far away are removed
                if (!OutOfBoundsProjectiles(i))
                {
                    for (int j = 0; j < m_Ships.Length; ++j)
                    {
                        // Ensure it's not the player ship!
                        // TODO: Not include the player ship in this grouping...
                        if (!Common.SameFloat3(m_Ships.Position[j].Value, PlayerController.em.GetComponentData<Position>(PlayerController.ship).Value))
                        {
                            if (Common.DistanceSquared(m_Projectiles.Position[i].Value, m_Ships.Position[j].Value) <= radius)
                            {
                                UpdateShipHealth(i, j);
                            }
                        }
                    }
                }
            }
            ClearProjectiles();
        }



        /// <summary>
        /// Checks projectile at the given index to see if it has left the playable area
        /// </summary>
        /// <param name="index">Index of projectile</param>
        /// <returns>bool</returns>
        private bool OutOfBoundsProjectiles(int index)
        {
            if (Common.DistanceSquared(m_Projectiles.Position[index].Value, Common.zero) > Settings.ProjectileMaxDistance)
            {
                Common.markedForDelete.Add(m_Projectiles.Position[index].Value);
                marked = true;
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Updates ship health if hit by a projectile
        /// </summary>
        /// <param name="projectile">Projectile index</param>
        /// <param name="ship">Ship index</param>
        private void UpdateShipHealth(int projectile, int ship)
        {
            if (Common.DistanceSquared(m_Projectiles.Position[projectile].Value, m_Ships.Position[ship].Value) <= radius)
            {
                float health = m_Ships.Health[ship].Value - (Settings.ProjectileDamage + CommanderShips.Settings.ProjectileDamageModifier);

                if (health > 0)
                {
                    m_Ships.Health[ship] = new Health { Value = health };
                }
                else
                {
                    Common.markedForDelete.Add(m_Ships.Position[ship].Value);
                    m_Ships.Velocity[ship] = new VelocityVector { Value = new float3(0f, 0f, 0f) };
                }
                Common.markedForDelete.Add(m_Projectiles.Position[projectile].Value);
                marked = true;
            }
        }



        /// <summary>
        /// Destroys the entity at the given position (do NOT approximate position!)
        /// </summary>
        private static void ClearProjectiles()
        {
            if (marked)
            {
                EntityManager em = World.Active.GetExistingManager<EntityManager>();
                var entities = em.GetAllEntities();
                for (int i = 0; i < entities.Length; ++i)
                {
                    if (Common.markedForDelete.Contains(em.GetComponentData<Position>(entities[i]).Value))
                    {
                        Common.markedForDelete.Remove(em.GetComponentData<Position>(entities[i]).Value);
                        em.DestroyEntity(entities[i]);
                    }
                }
                marked = false;
            }
        }
    }
}