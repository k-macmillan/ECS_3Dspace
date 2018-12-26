using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace Ships
{
    public static class Common
    {
        // Feel like I need a file of constants

        // Cockpit offsets:
        public static float3 green = new float3(0f, 1f, 3f);
        public static float3 red = new float3(0f, 1f, 4f);
        public static float green_theta = math.atan2(green.y, green.z);
        public static float red_theta = math.atan2(red.z, red.y);
        public static float green_mag = Vector3.Magnitude(green);
        public static float red_mag = Vector3.Magnitude(red);

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
                    return green + center;
                case Faction.red:
                    return red + center;
                default:
                    return new Vector3(center.x, center.y, center.z);
            }
        }

        /// <summary>
        /// Returns the mesh for the given string
        /// </summary>
        /// <param name="protoName">Component name</param>
        /// <returns></returns>
        public static MeshInstanceRenderer GetLookFromPrototype(string protoName)
        {
            var proto = GameObject.Find(protoName);
            var result = proto.GetComponent<MeshInstanceRendererComponent>().Value;
            Object.Destroy(proto);
            return result;
        }
    }

}