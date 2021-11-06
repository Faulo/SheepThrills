using UnityEngine;

namespace TheSheepGame.Player {
    public class InstantiateOnSpawnSheep : MonoBehaviour {
        [SerializeField]
        GameObject prefab = default;
        protected void OnEnable() {
            Herd.onSpawnSheep += HandleSpawnSheep;
        }
        protected void OnDisable() {
            Herd.onSpawnSheep -= HandleSpawnSheep;
        }
        void HandleSpawnSheep(Sheep obj) {
            Instantiate(prefab, obj.transform);
        }
    }
}
