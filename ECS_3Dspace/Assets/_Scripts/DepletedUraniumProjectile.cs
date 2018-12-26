using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{
    public static class DepletedUraniumProjectile
    {
        public static EntityArchetype wepDepletedUranium;

        public static MeshInstanceRenderer greenDepletedUranium;
        public static MeshInstanceRenderer redDepletedUranium;

        public static void GeneratProjectile(ref Entity projectile, ref EntityManager em, float3 position, int faction, float3 velocity)
        {
            projectile = em.CreateEntity(wepDepletedUranium);
            em.SetComponentData(projectile, new Position { Value = position });
            em.SetComponentData(projectile, new VelocityVector { Value = velocity });

            switch (faction)
            {
                case Faction.green:
                    em.SetComponentData(projectile, new PlayerFaction { Value = Faction.green });
                    em.AddSharedComponentData(projectile, greenDepletedUranium);
                    break;
                case Faction.red:
                    em.SetComponentData(projectile, new PlayerFaction { Value = Faction.red });
                    em.AddSharedComponentData(projectile, redDepletedUranium);
                    break;
                default:
                    Debug.Log("INVALID FACTION SELECTED: " + faction);
                    break;
            }
        }
    }
}