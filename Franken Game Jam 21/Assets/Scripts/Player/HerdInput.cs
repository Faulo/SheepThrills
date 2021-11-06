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
            actionsAsset["Destroy"].performed += OnDestroyPressed;
            actionsInstance.Enable();
        }

        protected void OnDisable() {
            if (actionsInstance) {
                actionsInstance.Disable();
                actionsAsset["Destroy"].performed -= OnDestroyPressed;
                Destroy(actionsInstance);
            }
        }

        protected void Update() {
            var move = actionsInstance["Move"].ReadValue<Vector2>();
            if (move != Vector2.zero) {
                herd.direction = move.normalized.SwizzleXZ();
            }
            if (actionsInstance["Bite"].WasPressedThisFrame()) {
                herd.Bite();
            }
        }

        private void OnDestroyPressed(InputAction.CallbackContext context) {
            if (!context.performed) {
                return;
            }
            //TODO: Herd.Bite()
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}