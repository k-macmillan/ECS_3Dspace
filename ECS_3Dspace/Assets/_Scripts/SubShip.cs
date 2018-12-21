using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{
    public static class SubShip
    {
        public static EntityArchetype subShipArchetype;

        public static MeshInstanceRenderer greenShips;
        public static MeshInstanceRenderer redShips;

        public static void GenerateShip(ref Entity ship, ref EntityManager em, float3 position, int faction, float3 velocity)
        {
            ship = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship, new Position { Value = position });
            em.SetComponentData(ship, new VelocityVector { Value = velocity });

            switch (faction)
            {
                case Faction.green:
                    em.SetComponentData(ship, new PositionModify { Orientation = new quaternion(0f, 0f, 0f, 1f), Thrust = 0f });
                    em.SetComponentData(ship, new HealthUpdate { Health = 100, Faction = Faction.green });
                    em.AddSharedComponentData(ship, greenShips);
                    break;
                case Faction.red:
                    em.SetComponentData(ship, new PositionModify { Orientation = new quaternion(0f, 1f, 0f, 0f), Thrust = 0f });
                    em.SetComponentData(ship, new HealthUpdate { Health = 100, Faction = Faction.red });
                    em.AddSharedComponentData(ship, redShips);
                    break;
                default:
                    Debug.Log("INVALID FACTION SELECTED: " + faction);
                    break;
            }
        }
    }

}