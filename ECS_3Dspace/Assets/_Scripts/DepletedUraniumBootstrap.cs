using Unity.Entities;
using Unity.Transforms;

namespace Ships
{
    public class DepletedUraniumBootstrap
    {
        public static EntityManager em;

        public void Initialize()
        {
            em = World.Active.GetOrCreateManager<EntityManager>();

            // Create ship archetype
            DepletedUraniumProjectile.wepDepletedUranium = em.CreateArchetype(
                // ComponentType.Create<CommanderShip.Commander>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<VelocityVector>(),
                ComponentType.Create<Faction>()
                );
        }
    }
}