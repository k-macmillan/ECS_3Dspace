using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ships
{
    public class Bootstrap
    {
        public static EntityManager em;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void CreateArchetypes()
        {
            em = World.Active.GetOrCreateManager<EntityManager>();

            SubShipBootstrap.Initialize(ref em);
            DepletedUraniumBootstrap.Initialize(ref em);
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void LoadMeshes()
        {
            SubShipBootstrap.InitializeWithScene();
            DepletedUraniumBootstrap.InitializeWithScene();

            NewGame();
        }


        public static void NewGame()
        {
            TEST_ShipGeneration.TEST_GenerateIdleShips(ref em);
            TEST_ShipGeneration.TEST_GenerateMovingShips(ref em);

            PlayerController player = new PlayerController();
            player.Start(new float3(-10, -10, -50f), Faction.green);
            DepletedUraniumProjectile.GenerateProjectile(ref em, new float3(-10f, -10f, 0f), Faction.red, new float3(0f, 0f, 0f));
        }
    }
}