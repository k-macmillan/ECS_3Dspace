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
            UpdateLook();
            UpdateOrientation();
        }


        private void UpdateLook()
        {
            yaw += Settings.MouseLookSensitivity * Input.GetAxis("Mouse X");
            pitch -= Settings.MouseLookSensitivity * Input.GetAxis("Mouse Y");
            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0f);

            Quaternion Q = em.GetComponentData<Rotation>(ship).Value;
            Vector3 pos = em.GetComponentData<Position>(ship).Value;
            camera.transform.position = Common.green_mag * math.cos(Common.green_theta) * Vector3.Normalize(Q * Vector3.forward) + pos;
        }

        private void UpdateOrientation()
        {


            quaternion orientation = camera.transform.rotation;

            PositionModify pm = new PositionModify { Orientation = orientation, Thrust = thrust };
            em.SetComponentData(ship, pm);
            em.SetComponentData(ship, new Rotation { Value = orientation });
        }
    }
}