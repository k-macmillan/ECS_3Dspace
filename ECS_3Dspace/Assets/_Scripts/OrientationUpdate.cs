using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ships
{
    public class OrientationUpdate : ComponentSystem
    {
        public struct Data
        {
            public readonly int Length;
            public ComponentDataArray<VelocityVector> Velocity;
            public ComponentDataArray<PositionModify> Orientation;
        }

        [Inject] private Data m_Data;

        protected override void OnUpdate()
        {
            float dt = Time.deltaTime;
            Quaternion q;
            float3 f;
            VelocityVector v;
            for (int i = 0; i < m_Data.Length; ++i)
            {
                // Cannot apply Vector3.forward to a math.quaternion
                q = m_Data.Orientation[i].Orientation;

                // Cannot drop it in, have to assign it a float3 first
                f = (q * Vector3.forward) * m_Data.Orientation[i].Thrust * dt;

                v = new VelocityVector { Value = m_Data.Velocity[i].Value + f };
                // Only update our velocity if we are under maximum speed
                if (Common.Magnitude(v.Value) < Settings.MaxShipSpeed)
                {
                    m_Data.Velocity[i] = v;
                }
            }
        }
    }
}