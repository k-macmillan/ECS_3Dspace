using UnityEngine;
using UnityEngine.UI;

namespace Ships
{

    public class HUDScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject Health;
        [SerializeField]
        private GameObject Speed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Health.GetComponent<Text>().text = "Health: " + PlayerData.Health.ToString();
            Speed.GetComponent<Text>().text = "Speed: " + PlayerData.Speed.ToString();
        }
    }
}