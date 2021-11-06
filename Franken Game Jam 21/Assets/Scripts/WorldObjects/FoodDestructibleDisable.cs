using System;
using UnityEngine;

namespace TheSheepGame.WorldObjects
{
    public class FoodDestructibleDisable : MonoBehaviour {
        [SerializeField] private FoodDestructible _destructible;
        [SerializeField] private SpriteRenderer _ownRenderer;
        [SerializeField] private BoxCollider _ownCollider;

        private void OnEnable() {
            _destructible.onObjectDestroyed += HandleDestruction;
        }

        private void OnDisable() {
            _destructible.onObjectDestroyed -= HandleDestruction;
        }

        private void HandleDestruction() {
            _ownRenderer.enabled = false;
            _ownCollider.enabled = false;
        }
    }
}
