using System.Collections.Generic;
using Cinemachine;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace TheSheepGame.Player {
    public class Herd : MonoBehaviour {
        [Header("MonoBehaviour configuration")]
        [SerializeField, Expandable]
        public CinemachineVirtualCamera virtualCamera = default;
        [SerializeField, Expandable]
        public CinemachineTargetGroup cameraGroup = default;

        [Header("Herd configuration")]
        [SerializeField]
        float maxSpeed = 5;
        [SerializeField]
        AnimationCurve speedOverCount = AnimationCurve.Constant(0, 1, 1);
        [SerializeField]
        float food = 0;
        [SerializeField]
        float foodPerSheep = 10;
        [SerializeField]
        int maxSheepCount = 100;

        [Header("Debug fields")]
        [SerializeField]
        public float speed = 0;
        [SerializeField]
        public Vector3 direction = Vector3.forward;

        public readonly List<Sheep> sheepList = new List<Sheep>();
        public int sheepCount => sheepList.Count;

        protected void Start() {
            foreach (var sheep in GetComponentsInChildren<Sheep>()) {
                AddSheep(sheep);
            }
        }

        protected void FixedUpdate() {
            food += Time.deltaTime;
            if (sheepList.Count < maxSheepCount && food > foodPerSheep) {
                food -= foodPerSheep;
                Multiply();
            }
        }

        public void Multiply() {
            var parent = sheepList.RandomElement();
            var child = Instantiate(parent, parent.transform.position + Random.insideUnitCircle.normalized.SwizzleXZ(), parent.transform.rotation, transform);
            AddSheep(child);
        }
        void AddSheep(Sheep sheep) {
            sheep.herd = this;
            sheepList.Add(sheep);
            cameraGroup.AddMember(sheep.transform, 1, 1);
            speed = maxSpeed * speedOverCount.Evaluate((float)sheepList.Count / maxSheepCount);
        }
    }
}
