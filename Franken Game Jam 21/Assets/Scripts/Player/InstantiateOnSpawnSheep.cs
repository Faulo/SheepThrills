using UnityEngine;

namespace TheSheepGame.Player {
    public class InstantiateOnSpawnSheep : MonoBehaviour {
        [SerializeField]
        GameObject prefab = default;
        [SerializeField]
        bool asChild = true;
        protected void OnEnable() {
            Herd.onSpawnSheep += HandleSpawnSheep;
        }
        protected void OnDisable() {
            Herd.onSpawnSheep -= HandleSpawnSheep;
        }
        void HandleSpawnSheep(Sheep sheep) {
            if (asChild) {
                Instantiate(prefab, sheep.transform);
            } else {
                Instantiate(prefab, sheep.transform.position, sheep.transform.rotation);
            }
        }
    }
}
