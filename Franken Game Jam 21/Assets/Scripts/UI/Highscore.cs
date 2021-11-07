using System;
using MyBox;
using TheSheepGame.Player;
using TheSheepGame.WorldObjects;
using UnityEngine;
using UnityEngine.UI;

namespace TheSheepGame.UI {
    public class Highscore : Singleton<Highscore> {

        [SerializeField] private GameObject _highscoreContainer;
        [SerializeField] private Text _sheepCreatedText;
        [SerializeField] private Text _sheepLostText;
        [SerializeField] private Text _tonsConsumedText;
        private int _sheepCreated;
        private int _sheepLost;
        private float _tonsConsumed;
        
        private void OnEnable() {
            Herd.onSpawnSheep += HandleSheepCreated;
            Herd.onDestroySheep += HandleSheepLost;
            Herd.onGainFood += HandleGainFood;
            Herd.onLose += HandleShowHighscoreScreen;
        }

        private void OnDisable() {
            Herd.onSpawnSheep -= HandleSheepCreated;
            Herd.onDestroySheep -= HandleSheepLost;
            Herd.onGainFood -= HandleGainFood;
            Herd.onLose -= HandleShowHighscoreScreen;
        }

        private void HandleSheepCreated(Sheep sheep) {
            _sheepCreated++;
        }

        private void HandleSheepLost(Sheep sheep) {
            _sheepLost++;
        }

        private void HandleGainFood(float amount) {
            _tonsConsumed += amount;
        }

        private void HandleShowHighscoreScreen() {
            _sheepCreatedText.text = "Sheep Created: " + _sheepCreated;
            _sheepLostText.text = "Sheep Lost: " + _sheepLost;
            _tonsConsumedText.text = "Tons Consumed: " + _tonsConsumed;
            _highscoreContainer.SetActive(true);
        }
    }
}