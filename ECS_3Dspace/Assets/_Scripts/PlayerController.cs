using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ships
{

    public class PlayerController : ComponentSystem
    {
        public static float roll;
        public static float pitch;
        public static float yaw;
        public static float thrust;
        public static Entity ship;

        private int faction;
        public static EntityManager em;
        public static GameObject camera;


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
            SubShip.GenerateShip(ref ship, ref em, Position, faction, new float3(0f, 0f, 0f));

            // Assign camera
            // Technically it will correct almost immediately due to OnUpdate
            camera = GameObject.Find("Main Camera").gameObject;
            camera.transform.position = Common.CockpitOffset(faction, Position);
            Cursor.lockState = CursorLockMode.Locked;
            thrust = 10f;
        }

        protected override void OnUpdate()
        {
            camera.transform.position = Common.CockpitOffset(faction, em.GetComponentData<Position>(ship).Value);

            yaw += Settings.MouseLookSensitivity * Input.GetAxis("Mouse X");
            pitch -= Settings.MouseLookSensitivity * Input.GetAxis("Mouse Y");
            Vector3 look_orientation = new Vector3(pitch, yaw, 1f);
            camera.transform.eulerAngles = look_orientation;
            quaternion orientation = camera.transform.rotation;

            PositionModify pm = new PositionModify { Orientation = orientation, Thrust = thrust };
            em.SetComponentData(ship, pm);
            em.SetComponentData(ship, new Rotation { Value = orientation });
        }
    }
}