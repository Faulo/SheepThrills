using System;
using System.Collections;
using TheSheepGame.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TheSheepGame.WorldObjects {
    public class FoodDestructible : MonoBehaviour {
        public static event Action<FoodDestructible> onObjectDestroyed;

        [SerializeField] private Image _canvasImage;
        [SerializeField] private int _foodAmount;
        [SerializeField] private int _neededSheepCount;
        [SerializeField] private float _areaRadius;
        [SerializeField] private int _currentSheepCount;
        private Coroutine _destroyRoutine;

        private void OnEnable() {
            Herd.onBite += OnBiteInput;
        }

        private void OnDisable() {
            Herd.onBite -= OnBiteInput;
        }

        private void OnBiteInput() {
            if (_currentSheepCount >= _neededSheepCount && _destroyRoutine == null) {
                _destroyRoutine = StartCoroutine(DestroyCoroutine());
            }
        }

        private void Update() {
            var colliders = Physics.OverlapSphere(transform.position, _areaRadius);
            _currentSheepCount = 0;
            for (var i = 0; i < colliders.Length; i++) {
                if (colliders[i].CompareTag("Sheep")) {
                    _currentSheepCount++;
                }
            }

            float fillAmount = 0f;
            if (_currentSheepCount < _neededSheepCount) {
                fillAmount = (float)_currentSheepCount / _neededSheepCount;
                _canvasImage.color = Color.white;
            } else {
                fillAmount = 1f;
                _canvasImage.color = Color.green;
            }

            _canvasImage.fillAmount = fillAmount;
        }

        private IEnumerator DestroyCoroutine() {
            Herd.Instance.GainFood(_foodAmount);
            onObjectDestroyed?.Invoke(this);
            yield return null;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _areaRadius);
        }
    }
}