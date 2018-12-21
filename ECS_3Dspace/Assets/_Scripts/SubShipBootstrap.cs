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
        public static EntityManager em;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            em = World.Active.GetOrCreateManager<EntityManager>();

            // Create ship archetype
            SubShip.subShipArchetype = em.CreateArchetype(
                // ComponentType.Create<CommanderShip.Commander>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<VelocityVector>(),
                ComponentType.Create<PositionModify>(),
                ComponentType.Create<HealthUpdate>()
                );
        }

        public static void NewGame()
        {
            TEST_ShipGeneration.TEST_GenerateIdleShips(ref em);
            TEST_ShipGeneration.TEST_GenerateMovingShips(ref em);

            PlayerController player = new PlayerController();
            player.Start(new float3(-10, -10, -50f), Faction.green);
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