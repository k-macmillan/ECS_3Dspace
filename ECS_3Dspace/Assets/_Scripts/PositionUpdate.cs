using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{
    // TODO: Should this utilize ComponentGroup instead of Data struct + Inject?
    // https://github.com/Unity-Technologies/EntityComponentSystemSamples/blob/master/Documentation/reference/component_group.md
    public class PositionUpdate : ComponentSystem
    {
        public struct Data
        {
            public readonly int Length;
            public ComponentDataArray<Position> Position;
            [ReadOnly] public ComponentDataArray<VelocityVector> Velocity;
        }

        [Inject] private Data m_Data;

        protected override void OnUpdate()
        {
            float dt = Time.deltaTime;
            for (int i = 0; i < m_Data.Length; ++i)
            {
                // next position = velocity * time + current position
                m_Data.Position[i] = new Position { Value = m_Data.Velocity[i].Value * dt + m_Data.Position[i].Value };
            }
        }
    }
}