using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheSheepGame.WorldObjects
{
    public class FoodDestructibleDisable : MonoBehaviour {
        [SerializeField] private FoodDestructible _destructible;
        [SerializeField] private List<SpriteRenderer> _renderers = new List<SpriteRenderer>();
        [SerializeField] private Collider _ownCollider;

        private void OnEnable() {
            _destructible.onObjectDestroyed += HandleDestruction;
        }

        private void OnDisable() {
            _destructible.onObjectDestroyed -= HandleDestruction;
        }

        private void HandleDestruction() {
            foreach (var renderer in _renderers) {
                renderer.enabled = false;
            }
            if (_ownCollider) {
                _ownCollider.enabled = false;
            }
        }
    }
}
