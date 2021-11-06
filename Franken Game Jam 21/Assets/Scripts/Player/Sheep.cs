using Slothsoft.UnityExtensions;
using UnityEngine;

namespace TheSheepGame.Player {
    public class Sheep : MonoBehaviour {
        [Header("MonoBehaviour configuration")]
        [SerializeField]
        public Herd herd = default;
        [SerializeField]
        public CharacterController character = default;

        [Header("Torque configuration")]
        [SerializeField, Range(0, 10)]
        float torqueSmoothing = 1;
        [SerializeField, Range(0, 1)]
        float herdRotationWeight = 0.5f;
        [SerializeField, Range(0, 1)]
        float inputRotationWeight = 0.5f;

        [Header("Movement configuration")]
        [SerializeField, Range(0, 10)]
        float herdVelocityWeight = 0.5f;
        [SerializeField, Range(0, 10)]
        float randomDirectionWeight = 0.5f;
        [SerializeField, Range(0, 10)]
        float separationWeight = 0.5f;
        [SerializeField, Range(0, 10)]
        float cohesionWeight = 0.5f;
        [SerializeField, Range(0, 10)]
        float repelWeight = 0.5f;

        [Header("Debug fields")]
        [SerializeField]
        public Vector2 position = Vector2.zero;
        [SerializeField]
        Vector2 velocity = Vector2.zero;
        [SerializeField]
        Vector2 separation = Vector2.zero;
        [SerializeField]
        Vector2 cohesion = Vector2.zero;
        [SerializeField]
        Vector2 repel = Vector2.zero;

        float torque;

        protected void Awake() {
            OnValidate();
        }
        protected void OnValidate() {
            if (!character) {
                TryGetComponent(out character);
            }
        }

        protected void FixedUpdate() {
            float currentAngle = transform.rotation.eulerAngles.y;
            float targetAngle = Quaternion.LookRotation(CalculateDirection(), Vector3.up).eulerAngles.y;
            float newAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref torque, torqueSmoothing);

            transform.rotation = Quaternion.Euler(0, newAngle, 0);

            velocity = CalculateVelocity();
            character.Move(velocity.SwizzleXZ() * Time.deltaTime);
        }
        Vector3 CalculateDirection() {
            return (herd.sheepDirection * herdRotationWeight)
                 + (herd.inputDirection * inputRotationWeight);
        }
        Vector2 CalculateVelocity() {
            position = transform.position.SwizzleXZ();

            separation = Vector2.zero;
            for (int i = 0; i < herd.sheepList.Count; i++) {
                var theirPosition = herd.sheepList[i].position;
                var delta = position - theirPosition;
                if (delta != Vector2.zero) {
                    separation += delta.normalized / Mathf.Max(0.01f, delta.sqrMagnitude);
                }
            }

            cohesion = herd.sheepCenter - position;

            repel = herd.CalculateRepel(position);

            return (herd.speed * herdVelocityWeight * transform.forward.SwizzleXZ())
                 + (cohesion * cohesionWeight)
                 + (separation * separationWeight)
                 + (repel * repelWeight)
                 + (Random.insideUnitCircle * randomDirectionWeight);
        }
    }
}
