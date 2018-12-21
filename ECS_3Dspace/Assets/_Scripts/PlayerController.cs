using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Ships
{

    public class PlayerController : ComponentSystem
    {
        public float roll;
        public float pitch;
        public float yaw;
        public float thrust;

        private Entity ship;
        private int faction;
        public static EntityManager em;


        // Start is called before the first frame update
        public void Start(float3 Position, int Faction)
        {
            Debug.Log("Player started...");
            em = SubShipBootstrap.em;
            faction = Faction;

            Initialize(Position);
        }

        private void Initialize(float3 position)
        {
            // Generate ship
            ship = SubShip.GenerateShip(ref em, position, faction, Common.zero);

            // Assign camera
            GameObject.Find("Main Camera").gameObject.transform.position = Common.CockpitOffset(faction, position);
        }

        protected override void OnUpdate()
        {
            Debug.Log("Updating...");
        }
    }
}