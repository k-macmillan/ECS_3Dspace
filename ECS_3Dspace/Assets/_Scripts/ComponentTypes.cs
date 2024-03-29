﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

/// <summary>
/// All ships share this namespace
/// </summary>
namespace Ships
{

    /* Component Systems
     * PositionUpdate: {Position, VelocityVector}
     * PositionModify: {Orientation, Thrust}
     * HealthUpdate: {Health, Faction}
     * 
     * Entity Archetypes
     * Bullets: Position, VelocityVector
     * SubShips: Position, VelocityVector, {Orientation, Thrust}, {Health, Faction}
     */

    public struct VelocityVector : IComponentData
    {
        public float3 Value;
    }

    public struct PositionModify : IComponentData
    {
        public quaternion Orientation;
        public float Thrust;
    }

    public struct Weapon : IComponentData
    {
        public float Value;
    }

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

    public struct PlayerFaction : IComponentData
    {
        public int Value;
    }

    public struct ShipFaction : IComponentData
    {
        public int Value;
    }

    public struct Mesh : IComponentData
    {
        public MeshInstanceRenderer Value;
    }
}

// TODO: Make commander ships
namespace CommanderShips
{

}