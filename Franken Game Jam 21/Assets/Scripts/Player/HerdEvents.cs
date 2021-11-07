using UnityEngine;
using UnityEngine.Events;

namespace TheSheepGame.Player {
    public class HerdEvents : MonoBehaviour {
        [SerializeField]
        UnityEvent onBite = new UnityEvent();

        protected void OnEnable() {
            Herd.onBite += onBite.Invoke;
        }
        protected void OnDisable() {
            Herd.onBite -= onBite.Invoke;
        }
    }
}
