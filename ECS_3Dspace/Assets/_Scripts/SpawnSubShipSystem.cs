using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{
    public class SpawnSubShipSystem : ComponentSystem
    {

        private static EntityManager entityManager;
        private static MeshInstanceRenderer entityRenderer;
        private static EntityArchetype entityArchetype;

        public struct Data
        {
            public readonly int Length;
            public ComponentDataArray<Position> Position;
            public ComponentDataArray<Thrust> Thrust;
            public ComponentDataArray<ForceVector> ForceVector;
            public ComponentDataArray<Rotation> Rotation;
        }

        [Inject] private Data m_Data;

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
            SpawnShip();
        }

        private static void SpawnShip()
        {
            //PostUpdateCommands.CreateEntity(SpawnEnemyShipSystem.EnemyShip)

        }

        protected override void OnUpdate()
        {
            float dt = Time.deltaTime;
            Quaternion Q;
            float3 adjustment;
            for (int index = 0; index < m_Data.Length; ++index)
            {
                var position = m_Data.Position[index].Value;
                var thrust = m_Data.Thrust[index].Value;
                var forceVector = m_Data.ForceVector[index].Value;
                var rotation = m_Data.Rotation[index].Value;

                Q = rotation;
                adjustment = Q * Vector3.forward * thrust;

                // Add the two "vectors" together to get the new vector
                position += dt * (forceVector + adjustment);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Starting SpawnSubShipSystem");
        }

    }

}