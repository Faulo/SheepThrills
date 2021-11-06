using System;
using System.Collections;
using Slothsoft.UnityExtensions;
using TheSheepGame.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TheSheepGame.WorldObjects {
    public class FoodDestructible : MonoBehaviour {
        public event Action<FoodDestructible> onObjectDestroyed;

        [SerializeField] private Image _canvasImage;
        [SerializeField] private int _foodAmount;
        [SerializeField] private int _neededSheepCount;
        [SerializeField] private float _areaRadius;
        [SerializeField] private int _currentSheepCount;
        private Coroutine _destroyRoutine;

        [SerializeField] LayerMask sheepLayer = default;
        static Collider[] colliders;

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

        protected void Start() {
            colliders = new Collider[Herd.Instance.maxSheepCount];
        }

        private void FixedUpdate() {
            _currentSheepCount = Physics.OverlapSphereNonAlloc(transform.position, _areaRadius, colliders, sheepLayer);

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