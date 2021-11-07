using MyBox;
using TheSheepGame.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TheSheepGame {
    public class GameManager : Singleton<GameManager> {
        [SerializeField]
        InputActionAsset actionsAsset = default;

        InputActionAsset actionsInstance;

        protected void OnEnable() {
            actionsInstance = Instantiate(actionsAsset);
            actionsInstance[nameof(Menu)].performed += Menu;
            actionsInstance.Enable();
        }
        protected void OnDisable() {
            if (actionsInstance) {
                actionsInstance.Disable();
                actionsInstance[nameof(Menu)].performed -= Menu;
                Destroy(actionsInstance);
            }
        }
        void Menu(InputAction.CallbackContext context) {
            var herd = FindObjectOfType<Herd>();
            if (herd) {
                herd.Lose();
            } else {
                Quit();
            }
        }
        public void Restart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void Quit() {
            var herd = FindObjectOfType<Herd>();
            if (herd) {
                herd.Lose();
            } else {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}
