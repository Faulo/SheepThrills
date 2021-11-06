using System;
using System.Collections.Generic;
using Cinemachine;
using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TheSheepGame.Player {
    public class Herd : Singleton<Herd> {
        public static event Action onBite;
        public static event Action<Sheep> onSpawnSheep;
        public static event Action<Sheep> onDestroySheep;

        [Header("MonoBehaviour configuration")]
        [SerializeField, Expandable]
        public CinemachineVirtualCamera virtualCamera = default;
        [SerializeField, Expandable]
        public CinemachineTargetGroup cameraGroup = default;
        [SerializeField, Expandable]
        public Light2D herdLight = default;
        [SerializeField, Expandable]
        public Sheep sheepPrefab = default;

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
        public int maxSheepCount = 100;
        [SerializeField]
        bool multiplyOnBite = false;

        [Header("Debug fields")]
        [SerializeField]
        public float speed = 0;
        [SerializeField]
        public Vector3 sheepDirection = Vector2.up;
        [SerializeField]
        public Vector3 inputDirection = Vector3.up;
        [SerializeField]
        public Vector2 sheepCenter = Vector2.zero;
        [SerializeField]
        float sheepRadius = 0;

        public readonly List<Sheep> sheepList = new List<Sheep>();
        public int sheepCount => sheepList.Count;

        protected void Start() {
            foreach (var sheep in GetComponentsInChildren<Sheep>()) {
                AddSheep(sheep);
            }
        }

        protected void FixedUpdate() {
            if (!multiplyOnBite) {
                food += Time.deltaTime;
                if (food > foodPerSheep) {
                    food -= foodPerSheep;
                    Multiply();
                }
            }
            sheepDirection = Vector3.zero;
            sheepCenter = Vector2.zero;
            for (int i = 0; i < sheepList.Count; i++) {
                sheepDirection += sheepList[i].transform.forward;
                sheepCenter += sheepList[i].position;
            }
            sheepDirection /= sheepCount;
            sheepCenter /= sheepCount;
            sheepRadius = cameraGroup.Sphere.radius;

            herdLight.pointLightOuterRadius = sheepRadius;
            herdLight.transform.position = sheepCenter.SwizzleXZ();
        }

        public void Multiply() {
            if (sheepList.Count >= maxSheepCount) {
                return;
            }
            var child = Instantiate(sheepPrefab, (sheepCenter + randomPointInHerd).SwizzleXZ(), Quaternion.identity, transform);
            AddSheep(child);
        }
        Vector2 randomPointInHerd {
            get {
                int value = UnityEngine.Random.Range(0, 360);
                return new Vector2(Mathf.Sin(value), Mathf.Cos(value)) * Mathf.Sqrt(sheepCount);
            }
        }
        void AddSheep(Sheep sheep) {
            sheep.herd = this;
            sheepList.Add(sheep);
            cameraGroup.AddMember(sheep.transform, 1, 1);
            speed = maxSpeed * speedOverCount.Evaluate((float)sheepList.Count / maxSheepCount);
            onSpawnSheep?.Invoke(sheep);
        }
        void RemoveSheep(Sheep sheep) {
            onDestroySheep?.Invoke(sheep);
        }
        public void Bite() {
            if (multiplyOnBite) {
                Multiply();
            }
            onBite?.Invoke();
        }

        public void GainFood(int amount) {
            food += amount;
        }
    }
}
