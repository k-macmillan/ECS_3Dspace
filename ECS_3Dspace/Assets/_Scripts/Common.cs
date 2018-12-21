using Unity.Mathematics;
using UnityEngine;

namespace Ships
{
    public static class Common
    {
        // Feel like I need a file of constants

        // Cockpit offsets:
        const float gx = 0.0f;
        const float gy = 1.0f;
        const float gz = 3.0f;
        const float rx = 0.0f;
        const float ry = 1.0f;
        const float rz = 4.0f;

        public static float3 zero;

        /// <summary>
        /// Finds and returns the squared magnitude of a given vector (float3)
        /// </summary>
        /// <param name="vector">float3 to check</param>
        /// <returns></returns>
        public static float MagnitudeSqrd(float3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }

        /// <summary>
        /// Finds and returns the magnitude of a given vector (float3)
        /// </summary>
        /// <param name="vector">float3 to check</param>
        /// <returns></returns>
        public static float Magnitude(float3 vector)
        {
            return math.sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }

        /// <summary>
        /// Offset from center to put the camera in the correct place
        /// </summary>
        /// <param name="faction">Faction being represented</param>
        /// <param name="center">Center of this camera</param>
        /// <returns></returns>
        public static float3 CockpitOffset(int faction, float3 center)
        {
            switch (faction)
            {
                case Faction.green:
                    return new Vector3(center.x + gx, center.y + gy, center.z + gz);
                case Faction.red:
                    return new Vector3(center.x + rx, center.y + ry, center.z + rz);
                default:
                    return new Vector3(center.x, center.y, center.z);
            }

        }
    }
}