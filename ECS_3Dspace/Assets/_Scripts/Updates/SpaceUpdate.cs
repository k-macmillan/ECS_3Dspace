using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Ships
{
    public class SpaceUpdate : ComponentSystem
    {
        public struct Ships
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Position> Position;
            public ComponentDataArray<Health> Health;
        }

        [Inject] private Ships m_Ships;

        protected override void OnUpdate()
        {
            var entities = Bootstrap.em.GetAllEntities();
        }
    }
}