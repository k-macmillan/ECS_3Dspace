using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{

    public class PlayerController : ComponentSystem
    {
        public float roll;
        public float pitch;
        public float yaw;
        public float thrust;
        public static Entity ship;

        private int faction;
        public static EntityManager em;


        // Start is called before the first frame update
        public void Start(float3 Position, int Faction)
        {
            em = SubShipBootstrap.em;
            faction = Faction;
            Initialize(Position);
        }

        private void Initialize(float3 Position)
        {
            // Generate ship
            SubShip.GenerateShip(ref ship, ref em, Position, faction, new float3(0f, 0f, 20f));

            // Assign camera
            // Technically it will correct almost immediately due to OnUpdate
            GameObject.Find("Main Camera").gameObject.transform.position = Common.CockpitOffset(faction, Position);
        }

        protected override void OnUpdate()
        {
            GameObject.Find("Main Camera").gameObject.transform.position = Common.CockpitOffset(faction, em.GetComponentData<Position>(ship).Value);
        }
    }
}