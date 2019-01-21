using UnityEngine;

namespace Ships {

    public class Pause : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PlayerSettings.Paused)
                {
                    ContinueGame();
                }
                else
                {
                    PauseGame();
                }
            }

            if (PlayerSettings.Paused && Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }

        private void ContinueGame()
        {
            Time.timeScale = 1f;
            PlayerSettings.Paused = false;
            Cursor.visible = false;
            HUDScript.Pause(false);
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            PlayerSettings.Paused = true;
            HUDScript.Pause(true);
        }
    }
}