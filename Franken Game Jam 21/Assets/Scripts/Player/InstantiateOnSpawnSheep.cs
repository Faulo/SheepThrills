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
        void HandleSpawnSheep(Sheep obj) {
            if (asChild) {
                Instantiate(prefab, obj.transform);
            } else {
                Instantiate(prefab, obj.transform.position, obj.transform.rotation);
            }
        }
    }
}
