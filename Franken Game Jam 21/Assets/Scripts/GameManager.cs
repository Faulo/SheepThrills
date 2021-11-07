using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSheepGame {
    public class GameManager : Singleton<GameManager> {
        public void LoadScene(string name) {
            SceneManager.LoadScene(name);
        }
        public void Restart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void Quit() {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
