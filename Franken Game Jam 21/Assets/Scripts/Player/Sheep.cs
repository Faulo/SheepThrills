using Slothsoft.UnityExtensions;
using UnityEngine;

namespace TheSheepGame.Player {
    public class Sheep : MonoBehaviour {
        [Header("MonoBehaviour configuration")]
        [SerializeField]
        public Herd herd = default;
        [SerializeField]
        public CharacterController character = default;

        [Header("Movement configuration")]
        [SerializeField, Range(0, 1)]
        float useInputDirection = 0.5f;
        [SerializeField, Range(0, 10)]
        float herdVelocityWeight = 0.5f;
        [SerializeField, Range(0, 10)]
        float randomDirectionWeight = 0.5f;
        [SerializeField, Range(0, 10)]
        float separationWeight = 0.5f;
        [SerializeField, Range(0, 10)]
        float cohesionWeight = 0.5f;

        [Header("Debug fields")]
        [SerializeField]
        Vector2 velocity = Vector3.zero;
        [SerializeField]
        Vector2 cohesion = Vector3.zero;
        [SerializeField]
        Vector2 separation = Vector3.zero;

        protected void Awake() {
            OnValidate();
        }
        protected void OnValidate() {
            if (!character) {
                TryGetComponent(out character);
            }
        }
        protected void Start() {
        }

        protected void FixedUpdate() {
            transform.rotation = Quaternion.LookRotation(CalculateDirection(), Vector3.up);

            velocity = CalculateVelocity();
            character.Move(velocity.SwizzleXZ() * Time.deltaTime);
        }
        Vector3 CalculateDirection() {
            var direction = Vector3.zero;
            for (int i = 0; i < herd.sheepList.Count; i++) {
                direction += herd.sheepList[i].transform.forward;
            }
            direction /= herd.sheepCount;
            return Vector3.Lerp(direction, herd.direction, useInputDirection);
        }
        Vector2 CalculateVelocity() {
            var myPosition = transform.position.SwizzleXZ();
            cohesion = Vector2.zero;
            separation = Vector2.zero;
            for (int i = 0; i < herd.sheepList.Count; i++) {
                var theirPosition = herd.sheepList[i].transform.position.SwizzleXZ();
                cohesion += theirPosition;
                var delta = myPosition - theirPosition;
                if (delta != Vector2.zero) {
                    separation += delta.normalized / delta.sqrMagnitude;
                }
            }
            cohesion /= herd.sheepCount;
            cohesion -= myPosition;

            return (herd.speed * herdVelocityWeight * transform.forward.SwizzleXZ())
                 + (cohesion * cohesionWeight)
                 + (separation * separationWeight)
                 + (Random.insideUnitCircle * randomDirectionWeight);
        }
    }
}
