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

        public static GameObject PausePanel;

        // Start is called before the first frame update
        void Start()
        {
            PausePanel = GameObject.Find("HUDCanvas/PausedPanel");
            PausePanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            Health.GetComponent<Text>().text = "Health: " + PlayerData.Health.ToString();
            Speed.GetComponent<Text>().text = "Speed: " + PlayerData.Speed.ToString();
        }

        public static void Pause(bool pause)
        {
            PausePanel.SetActive(pause);
        }
    }
}