using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// All ships share this namespace
/// </summary>
namespace Ships
{
    /// <summary>
    /// Sub ship health
    /// </summary>
    public struct Health : IComponentData
    {
        public float Value;
    }

    ///// <summary>
    ///// Sub ship weapon cooldown
    ///// </summary>
    //public struct Cooldown : IComponentData
    //{
    //    // May change if commander boosts a cohort. Not sure yet.
    //    public float Value;
    //}

    /// <summary>
    /// Sub ship mass
    /// </summary>
    public struct Mass : IComponentData
    {
        // May change if commander boosts a cohort. Not sure yet.
        public float Value;
    }

    /// <summary>
    /// Sub ship thrust
    /// </summary>
    public struct Thrust : IComponentData
    {
        /// <summary>
        /// Range: [-1.0, 1.0]
        /// </summary>
        public float Value;
    }

    /// <summary>
    /// Sub ship force and direction
    /// </summary>
    public struct ForceVector : IComponentData
    {
        public float mass;
        /// <summary>
        /// Direction and force of acceleration
        /// </summary>
        public float3 acceleration;
        /// <summary>
        /// Acceleration applied to existing force vector
        /// </summary>
        public float3 Value;
    }

    // Constant after creation.

    /// <summary>
    /// Sub ship team
    /// </summary>
    public struct Faction : IComponentData
    {
        public const int green = 0;
        public const int red = 1;
    }

}

// TODO: Make commander ships
namespace CommanderShips
{

}