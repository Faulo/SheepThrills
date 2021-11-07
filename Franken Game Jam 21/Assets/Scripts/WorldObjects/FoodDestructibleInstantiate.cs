using UnityEngine;

namespace TheSheepGame.WorldObjects {
    public class FoodDestructibleInstantiate : MonoBehaviour {
        [SerializeField]
        FoodDestructible destructible = default;
        [SerializeField]
        GameObject prefab = default;
        [SerializeField]
        bool asChild = true;
        protected void Awake() {
            OnValidate();
        }
        protected void OnValidate() {
            if (!destructible) {
                destructible = GetComponentInParent<FoodDestructible>();
            }
        }
        protected void OnEnable() {
            destructible.onObjectDestroyed += HandleObjectDestroyed;
        }
        protected void OnDisable() {
            destructible.onObjectDestroyed -= HandleObjectDestroyed;
        }
        void HandleObjectDestroyed() {
            if (asChild) {
                Instantiate(prefab, transform);
            } else {
                Instantiate(prefab, transform.position, transform.rotation);
            }
        }
    }
}
