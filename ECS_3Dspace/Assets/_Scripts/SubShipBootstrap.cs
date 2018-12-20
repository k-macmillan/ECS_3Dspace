using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ships
{
    public sealed class SubShipBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            // Create ship archetype
            SubShip.subShipArchetype = entityManager.CreateArchetype(
                // ComponentType.Create<CommanderShip.Commander>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<VelocityVector>(),
                ComponentType.Create<PositionModify>(),
                ComponentType.Create<HealthUpdate>()
                );
        }

        public static void NewGame()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            TEST_ShipGeneration.TEST_GenerateIdleShips(ref entityManager);
            TEST_ShipGeneration.TEST_GenerateMovingShips(ref entityManager);


        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeAfterSceneLoad()
        {
            InitializeWithScene();
        }


        public static void InitializeWithScene()
        {

            SubShip.greenShips = GetLookFromPrototype("GreenShipPrototype");
            SubShip.redShips = GetLookFromPrototype("RedShipPrototype");

            //EnemySpawnSystem.SetupComponentData(World.Active.GetOrCreateManager<EntityManager>());

            //World.Active.GetOrCreateManager<UpdatePlayerHUD>().SetupGameObjects();

            NewGame();
        }



        private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            InitializeWithScene();
        }

        /// <summary>
        /// Returns the mesh for the given string
        /// </summary>
        /// <param name="protoName">Component name</param>
        /// <returns></returns>
        private static MeshInstanceRenderer GetLookFromPrototype(string protoName)
        {
            var proto = GameObject.Find(protoName);
            var result = proto.GetComponent<MeshInstanceRendererComponent>().Value;
            Object.Destroy(proto);
            return result;
        }
    }

}