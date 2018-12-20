using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ships
{
    public sealed class SubShipBootstrap
    {

        /// <summary>
        /// Sub ship archetype
        /// </summary>
        public static EntityArchetype subShipArchetype;

        public static MeshInstanceRenderer greenShips;
        public static MeshInstanceRenderer redShips;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            // Create ship archetype
            subShipArchetype = entityManager.CreateArchetype(
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
            Debug.Log("Spawning ships");

            // Create 1000 hardcoded ships for testing
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 50; ++j)
                {
                    if (i % 2 == 0)
                    {
                        TEST_GenerateShip(ref entityManager, new float3(j * 15f + 5f, i * 15f, -20f), true);
                        TEST_GenerateShip(ref entityManager, new float3(j * 15f + 5f, i * 15f, 20f), false);
                    }
                    else
                    {
                        TEST_GenerateShip(ref entityManager, new float3(j * 15f, i * 15f, -25f), true);
                        TEST_GenerateShip(ref entityManager, new float3(j * 15f, i * 15f, 25f), false);
                    }
                }
            }

        }

        private static void TEST_GenerateShip(ref EntityManager em, float3 position, bool green = false)
        {
            Entity ship = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship, new Position { Value = position });
            em.SetComponentData(ship, new VelocityVector { Value = new float3(0f, 0f, 0f) });
            

            if (green)
            {
                em.SetComponentData(ship, new PositionModify { Orientation = new quaternion(0f, 0f, 0f, 1f), Thrust = 0f });
                em.SetComponentData(ship, new HealthUpdate { Health = 100, Faction = Faction.green });
                em.AddSharedComponentData(ship, greenShips);
            }
            else
            {
                em.SetComponentData(ship, new PositionModify { Orientation = new quaternion(0f, 1f, 0f, 0f), Thrust = 0f });
                em.SetComponentData(ship, new HealthUpdate { Health = 100, Faction = Faction.red });
                em.AddSharedComponentData(ship, redShips);
            }


        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeAfterSceneLoad()
        {
            InitializeWithScene();
        }


        public static void InitializeWithScene()
        {

            greenShips = GetLookFromPrototype("GreenShipPrototype");
            redShips = GetLookFromPrototype("RedShipPrototype");

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