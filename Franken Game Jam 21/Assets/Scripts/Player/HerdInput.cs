using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheSheepGame.Player {
    public class HerdInput : MonoBehaviour {
        [SerializeField] Herd herd = default;
        [SerializeField] InputActionAsset actionsAsset = default;

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
            actionsInstance["Bite"].performed += OnBitePressed;
            actionsInstance.Enable();
        }

        protected void OnDisable() {
            if (actionsInstance) {
                actionsInstance.Disable();
                actionsInstance["Bite"].performed -= OnBitePressed;
                Destroy(actionsInstance);
            }
        }

        protected void Update() {
            var move = actionsInstance["Move"].ReadValue<Vector2>();
            if (move != Vector2.zero) {
                herd.direction = move.normalized.SwizzleXZ();
            }
        }

        private void OnBitePressed(InputAction.CallbackContext context) {
            if (!context.performed) {
                return;
            }
            herd.Bite();
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}