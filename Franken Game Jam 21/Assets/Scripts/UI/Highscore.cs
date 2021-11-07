using MyBox;
using TheSheepGame.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheSheepGame.UI {
    public class Highscore : Singleton<Highscore> {

        [SerializeField] GameObject _highscoreContainer;
        [SerializeField] Text _sheepCreatedText;
        [SerializeField] Text _sheepLostText;
        [SerializeField] Text _sheepMaximumText;
        [SerializeField] Text _tonsConsumedText;
        int _sheepCreated;
        int _sheepLost;
        int _sheepMaximum;
        float _tonsConsumed;

        protected void OnEnable() {
            Herd.onSpawnSheep += HandleSheepCreated;
            Herd.onDestroySheep += HandleSheepLost;
            Herd.onGainFood += HandleGainFood;
            Herd.onLose += HandleShowHighscoreScreen;
        }

        protected void OnDisable() {
            Herd.onSpawnSheep -= HandleSheepCreated;
            Herd.onDestroySheep -= HandleSheepLost;
            Herd.onGainFood -= HandleGainFood;
            Herd.onLose -= HandleShowHighscoreScreen;
        }

        void HandleSheepCreated(Sheep sheep) {
            _sheepCreated++;
            if (_sheepMaximum < sheep.herd.sheepCount) {
                _sheepMaximum = sheep.herd.sheepCount;
            }
        }

        void HandleSheepLost(Sheep sheep) {
            _sheepLost++;
        }

        void HandleGainFood(float amount) {
            _tonsConsumed += amount;
        }

        void HandleShowHighscoreScreen() {
            if (_sheepCreatedText) {
                _sheepCreatedText.text += _sheepCreated;
            }
            if (_sheepLostText) {
                _sheepLostText.text += _sheepLost;
            }
            if (_sheepMaximumText) {
                _sheepMaximumText.text += _sheepMaximum;
            }
            if (_tonsConsumedText) {
                _tonsConsumedText.text += _tonsConsumed;
            }

            _highscoreContainer.SetActive(true);

            EventSystem.current.SetSelectedGameObject(_highscoreContainer.GetComponentInChildren<Selectable>().gameObject);
        }

        public void Restart() {
            GameManager.Instance.Restart();
        }
        public void Quit() {
            GameManager.Instance.Quit();
        }
    }
}