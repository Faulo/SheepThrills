using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace TheSheepGame.WorldObjects
{
    public class FoodDestructibleVFX : MonoBehaviour {

        [SerializeField] private List<VisualEffect> _visualEffects;
        [SerializeField] private FoodDestructible _foodDestructible;
        private void OnEnable() {
            _foodDestructible.onObjectDestroyed += HandleDestructibleDestroyed;
        }

        private void OnDisable() {
            _foodDestructible.onObjectDestroyed -= HandleDestructibleDestroyed;
        }

        private void HandleDestructibleDestroyed() {
            foreach (var visualEffect in _visualEffects) {
                visualEffect.Play();
            }
        }
    }
}
