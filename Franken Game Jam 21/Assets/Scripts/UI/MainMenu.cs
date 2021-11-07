using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSheepGame
{
    public class MainMenu : MonoBehaviour
    {
        public void HandleStartGame() {
            SceneManager.LoadScene("Level_1");
        }

        public void HandleQuitGame() {
            Application.Quit();
        }
    }
}
