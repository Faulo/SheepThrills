using TheSheepGame.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TheSheepGame {
    public class Grass : MonoBehaviour {
        [SerializeField]
        Tilemap tilemap = default;

        protected void Awake() {
            OnValidate();
            tilemap.RefreshAllTiles();
        }

        protected void OnValidate() {
            if (!tilemap) {
                TryGetComponent(out tilemap);
            }
        }

        protected void OnEnable() {
            Herd.onBite += OnHandleBite;
        }
        protected void OnDisable() {
            Herd.onBite -= OnHandleBite;
        }

        void OnHandleBite() {
            for (int i = 0; i < Herd.Instance.sheepCount; i++) {
                var position = tilemap.WorldToCell(Herd.Instance.sheepList[i].transform.position);
                if (tilemap.HasTile(position)) {
                    tilemap.SetTile(position, default);
                    Herd.Instance.EatGrass();
                }
            }
        }
    }
}