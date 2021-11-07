using UnityEngine;
using UnityEngine.Tilemaps;

namespace TheSheepGame {
    public class Border : MonoBehaviour {
        [Header("MonoBehaviour configuration")]
        [SerializeField]
        Grid grid = default;
        [SerializeField]
        Tilemap tilemap = default;

        [Header("Tilemap configuration")]
        [SerializeField]
        Vector3 cellSize = Vector3.zero;
        [SerializeField]
        Vector3 cellOffset = Vector3.zero;
        [SerializeField]
        Vector3 cellGap = Vector3.zero;

        protected void Awake() {
            OnValidate();
            tilemap.RefreshAllTiles();
        }
        protected void OnValidate() {
            if (!grid) {
                grid = GetComponentInParent<Grid>();
            }
            if (!tilemap) {
                TryGetComponent(out tilemap);
            }
            grid.cellSize = cellSize;
            grid.cellGap = cellGap;
            grid.transform.localPosition = cellOffset;
        }
        protected void Start() {
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                if (tilemap.HasTile(position)) {
                    CreateCollider(tilemap.CellToWorld(position + new Vector3Int(0, 1, 0)));
                }
            }
        }
        void CreateCollider(Vector3 position) {
            var box = gameObject.AddComponent<BoxCollider>();
            box.center = position;
            box.size = new Vector3(cellSize.x, 1, cellSize.y);
        }
    }
}
