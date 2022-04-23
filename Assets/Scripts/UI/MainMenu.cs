using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSheepGame {
    public class MainMenu : MonoBehaviour {
        public void HandleStartGame() {
            SceneManager.LoadScene("Level_1");
        }
    }
}
