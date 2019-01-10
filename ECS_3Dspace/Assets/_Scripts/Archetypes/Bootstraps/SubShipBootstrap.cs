using Unity.Entities;
using Unity.Transforms;
using UnityEngine.SceneManagement;

namespace Ships
{
    public sealed class SubShipBootstrap
    {
        public static EntityManager em;

        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize(ref EntityManager entityManager)
        {
            em = entityManager;

            // Create ship archetype
            SubShip.subShipArchetype = em.CreateArchetype(
                // ComponentType.Create<CommanderShip.Commander>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<Rotation>(),
                ComponentType.Create<VelocityVector>(),
                ComponentType.Create<PositionModify>(),
                ComponentType.Create<Health>(),
                ComponentType.Create<Weapon>(),
                ComponentType.Create<ShipFaction>()
                );
        }

        public static void InitializeWithScene()
        {
            SubShip.greenShips = Common.GetLookFromPrototype("GreenShipPrototype");
            SubShip.redShips = Common.GetLookFromPrototype("RedShipPrototype");
        }



        private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            InitializeWithScene();
        }
    }
}