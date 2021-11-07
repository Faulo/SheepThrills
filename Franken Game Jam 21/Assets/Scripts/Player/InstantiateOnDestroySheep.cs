using UnityEngine;

namespace TheSheepGame.Player {
    public class InstantiateOnDestroySheep : MonoBehaviour {
        [SerializeField]
        GameObject prefab = default;
        [SerializeField]
        bool asChild = true;
        protected void OnEnable() {
            Herd.onDestroySheep += HandleDestroySheep;
        }
        protected void OnDisable() {
            Herd.onDestroySheep -= HandleDestroySheep;
        }
        void HandleDestroySheep(Sheep sheep) {
            if (asChild) {
                Instantiate(prefab, sheep.transform);
            } else {
                Instantiate(prefab, sheep.transform.position, sheep.transform.rotation);
            }
        }
    }
}
