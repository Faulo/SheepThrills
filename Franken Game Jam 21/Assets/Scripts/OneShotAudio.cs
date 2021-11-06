using UnityEngine;

namespace TheSheepGame {
    public class OneShotAudio : MonoBehaviour {
        [SerializeField]
        AudioSource audioSource = default;
        protected void Awake() {
            OnValidate();
        }
        protected void OnValidate() {
            if (!audioSource) {
                TryGetComponent(out audioSource);
            }
        }
        protected void Update() {
            if (!audioSource.isPlaying) {
                Destroy(gameObject);
            }
        }
    }
}
