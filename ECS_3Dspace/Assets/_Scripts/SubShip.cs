using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{

    public sealed class SubShip
    {

        /// <summary>
        /// Sub ship archetype
        /// </summary>
        public static EntityArchetype subShipArchetype;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            // Create ship archetype
            subShipArchetype = entityManager.CreateArchetype(
                // ComponentType.Create<CommanderShip.Commander>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<Rotation>(),
                ComponentType.Create<Health>(),
                //ComponentType.Create<Cooldown>(),
                ComponentType.Create<ForceVector>(),
                ComponentType.Create<Thrust>(),
                ComponentType.Create<Mass>(),
                ComponentType.Create<Faction>());
        }

    }

}