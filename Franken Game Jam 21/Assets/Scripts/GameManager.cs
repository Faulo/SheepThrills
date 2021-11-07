using System.Collections;
using MyBox;
using Slothsoft.UnityExtensions;
using TheSheepGame.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSheepGame {
    public class GameManager : Singleton<GameManager> {
        [SerializeField, Range(0, 10)]
        float restartTimeout = 1;

        protected void OnEnable() {
            Herd.onLose += HandleLose;
        }
        protected void OnDisable() {
            Herd.onLose -= HandleLose;
        }
        void HandleLose() {
            StartCoroutine(RestartRoutine());
        }
        IEnumerator RestartRoutine() {
            yield return Wait.forSeconds[restartTimeout];
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
