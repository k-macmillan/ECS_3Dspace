using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ships
{

    public sealed class SubShip
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
                ComponentType.Create<Rotation>(),
                ComponentType.Create<Health>(),
                //ComponentType.Create<Cooldown>(),
                ComponentType.Create<ForceVector>(),
                ComponentType.Create<Thrust>(),
                ComponentType.Create<Mass>(),
                ComponentType.Create<PlayerData>());
        }

        public static void NewGame()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();

            // Create some hardcoded ships for testing
            TEST_GenerateRedShips(ref entityManager);
            TEST_GenerateGreeShips(ref entityManager);

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



        /// <summary>
        /// Generates some red ships for testing
        /// </summary>
        /// <param name="em">EntityManager</param>
        private static void TEST_GenerateRedShips(ref EntityManager em)
        {
            float3 forceVec = new float3( 0f, 0f, 0f );
            quaternion direction = new quaternion(0f, 1f, 0f, 0f);  // Opposite of quaternion.identity
            // Ship 0
            Entity ship0 = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship0, new Position { Value = new float3(10.0f, 10.0f, 10.0f) });
            em.SetComponentData(ship0, new Rotation { Value = direction });
            em.SetComponentData(ship0, new Health { Value = 100 });
            em.SetComponentData(ship0, new ForceVector { Value = forceVec });
            em.SetComponentData(ship0, new Thrust { Value = 1 });
            em.SetComponentData(ship0, new Mass { Value = 1 });
            em.SetComponentData(ship0, new PlayerData { Faction = Faction.red });

            // Ship 1
            Entity ship1 = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship1, new Position { Value = new float3(20.0f, 20.0f, 20.0f) });
            em.SetComponentData(ship1, new Rotation { Value = direction });
            em.SetComponentData(ship1, new Health { Value = 100 });
            em.SetComponentData(ship1, new ForceVector { Value = forceVec });
            em.SetComponentData(ship1, new Thrust { Value = 1 });
            em.SetComponentData(ship1, new Mass { Value = 1 });
            em.SetComponentData(ship1, new PlayerData { Faction = Faction.red });

            // Ship 2
            Entity ship2 = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship2, new Position { Value = new float3(30.0f, 30.0f, 30.0f) });
            em.SetComponentData(ship2, new Rotation { Value = direction });
            em.SetComponentData(ship2, new Health { Value = 100 });
            em.SetComponentData(ship2, new ForceVector { Value = forceVec });
            em.SetComponentData(ship2, new Thrust { Value = 1 });
            em.SetComponentData(ship2, new Mass { Value = 1 });
            em.SetComponentData(ship2, new PlayerData { Faction = Faction.red });

            // Add ship renderers
            em.AddSharedComponentData(ship0, redShips);
            em.AddSharedComponentData(ship1, redShips);
            em.AddSharedComponentData(ship2, redShips);
        }



        /// <summary>
        /// Generates some green ships for testing
        /// </summary>
        /// <param name="em">EntityManager</param>
        private static void TEST_GenerateGreeShips(ref EntityManager em)
        {
            float3 forceVec = new float3(0f, 0f, 0f);
            // Ship 0
            Entity ship0 = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship0, new Position { Value = new float3(10.0f, 10.0f, -10.0f) });
            em.SetComponentData(ship0, new Rotation { Value = quaternion.identity });
            em.SetComponentData(ship0, new Health { Value = 100 });
            em.SetComponentData(ship0, new ForceVector { Value = forceVec });
            em.SetComponentData(ship0, new Thrust { Value = 1 });
            em.SetComponentData(ship0, new Mass { Value = 1 });
            em.SetComponentData(ship0, new PlayerData { Faction = Faction.green });

            // Ship 1
            Entity ship1 = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship1, new Position { Value = new float3(20.0f, 20.0f, -20.0f) });
            em.SetComponentData(ship1, new Rotation { Value = quaternion.identity });
            em.SetComponentData(ship1, new Health { Value = 100 });
            em.SetComponentData(ship1, new ForceVector { Value = forceVec });
            em.SetComponentData(ship1, new Thrust { Value = 1 });
            em.SetComponentData(ship1, new Mass { Value = 1 });
            em.SetComponentData(ship1, new PlayerData { Faction = Faction.green });

            // Ship 2
            Entity ship2 = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship2, new Position { Value = new float3(30.0f, 30.0f, -30.0f) });
            em.SetComponentData(ship2, new Rotation { Value = quaternion.identity });
            em.SetComponentData(ship2, new Health { Value = 100 });
            em.SetComponentData(ship2, new ForceVector { Value = forceVec });
            em.SetComponentData(ship2, new Thrust { Value = 1 });
            em.SetComponentData(ship2, new Mass { Value = 1 });
            em.SetComponentData(ship2, new PlayerData { Faction = Faction.green });

            // Add ship renderers
            em.AddSharedComponentData(ship0, greenShips);
            em.AddSharedComponentData(ship1, greenShips);
            em.AddSharedComponentData(ship2, greenShips);
        }

    }

}