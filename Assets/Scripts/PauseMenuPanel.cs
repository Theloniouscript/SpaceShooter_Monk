using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class PauseMenuPanel : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonShowPause()
        {
            Time.timeScale = 0; // чтобы все объекты на сцене перестали двигаться
            gameObject.SetActive(true);
        }

        public void OnButtonContinue()
        {
            Time.timeScale = 1; 
            gameObject.SetActive(false);
        }

        public void OnButtonMenu()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickname);
        }

    }

}
