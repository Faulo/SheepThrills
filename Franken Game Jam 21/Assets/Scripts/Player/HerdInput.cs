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
            actionsInstance[nameof(Move)].performed += Move;
            actionsInstance[nameof(Bite)].performed += Bite;
            actionsInstance.Enable();
        }

        protected void OnDisable() {
            if (actionsInstance) {
                actionsInstance.Disable();
                actionsInstance[nameof(Move)].performed -= Move;
                actionsInstance[nameof(Bite)].performed -= Bite;
                Destroy(actionsInstance);
            }
        }

        void Move(InputAction.CallbackContext context) {
            herd.direction = context.ReadValue<Vector2>();
        }

        void Bite(InputAction.CallbackContext context) {
            herd.Bite();
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}