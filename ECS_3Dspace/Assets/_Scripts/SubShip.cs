using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Ships
{
    public static class SubShip
    {
        public static EntityArchetype subShipArchetype;

        public static MeshInstanceRenderer greenShips;
        public static MeshInstanceRenderer redShips;

        public static void GenerateIdleShip(ref EntityManager em, float3 position, bool green, float3 velocity)
        {
            Entity ship = em.CreateEntity(subShipArchetype);
            em.SetComponentData(ship, new Position { Value = position });
            em.SetComponentData(ship, new VelocityVector { Value = velocity });

            if (green)
            {
                em.SetComponentData(ship, new PositionModify { Orientation = new quaternion(0f, 0f, 0f, 1f), Thrust = 0f });
                em.SetComponentData(ship, new HealthUpdate { Health = 100, Faction = Faction.green });
                em.AddSharedComponentData(ship, greenShips);
            }
            else
            {
                em.SetComponentData(ship, new PositionModify { Orientation = new quaternion(0f, 1f, 0f, 0f), Thrust = 0f });
                em.SetComponentData(ship, new HealthUpdate { Health = 100, Faction = Faction.red });
                em.AddSharedComponentData(ship, redShips);
            }
        }
    }

}