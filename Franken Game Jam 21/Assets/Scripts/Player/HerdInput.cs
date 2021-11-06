using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheSheepGame.Player {
    public class HerdInput : MonoBehaviour {
        [SerializeField]
        Herd herd = default;
        [SerializeField]
        InputActionAsset actionsAsset = default;

        InputActionAsset actionsInstance;

        protected void Awake() {
            OnValidate();
        }
        protected void OnValidate() {
            if (!herd) {
                TryGetComponent(out herd);
            }
        }
        protected void OnEnable() {
            actionsInstance = Instantiate(actionsAsset);
            actionsInstance.Enable();
        }
        protected void OnDisable() {
            if (actionsInstance) {
                actionsInstance.Disable();
                Destroy(actionsInstance);
            }
        }
        protected void Update() {
            var move = actionsInstance["Move"].ReadValue<Vector2>();
            if (move != Vector2.zero) {
                herd.direction = move.normalized.SwizzleXZ();
            }
        }
        protected void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}