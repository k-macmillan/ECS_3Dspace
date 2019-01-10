using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{
    public sealed class DepletedUraniumBootstrap
    {
        public static EntityManager em;

        public static void Initialize(ref EntityManager entityManager)
        {
            em = entityManager;

            // Create projectile archetype
            DepletedUraniumProjectile.wepDepletedUranium = em.CreateArchetype(
                // ComponentType.Create<CommanderShip.Commander>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<VelocityVector>(),
                ComponentType.Create<PlayerFaction>()
                );
        }


        public static void InitializeWithScene()
        {
            DepletedUraniumProjectile.greenDepletedUranium = Common.GetLookFromPrototype("GreenProjectilePrototype");
            DepletedUraniumProjectile.redDepletedUranium = Common.GetLookFromPrototype("RedProjectilePrototype");
        }
    }
}