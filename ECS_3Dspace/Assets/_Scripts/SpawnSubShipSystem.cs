using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

namespace Ships
{
    public class SpawnSubShipSystem : ComponentSystem
    {

        private static EntityManager entityManager;
        private static MeshInstanceRenderer entityRenderer;
        private static EntityArchetype entityArchetype;

        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeOnLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            entityManager = World.Active.GetOrCreateManager<EntityManager>();
            entityArchetype = entityManager.CreateArchetype(
                typeof(Position));
        }

        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeOnLoadType.AfterSceneLoad)]
        public static void InitializeWithScene()
        {
            // entityRenderer = 
            SpawnShip();
        }

        private static void SpawnShip()
        {
            //PostUpdateCommands.CreateEntity(SpawnEnemyShipSystem.EnemyShip)

        }

        protected override void OnUpdate()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

    }

}