using Unity.Mathematics;

namespace Ships
{
    public static class Common
    {
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
    }
}