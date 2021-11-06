using System.Collections.Generic;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace TheSheepGame.Player {
    public class Herd : MonoBehaviour {
        [SerializeField]
        public Vector3 direction = Vector3.forward;
        [SerializeField]
        public float speed = 5;
        [SerializeField]
        float food = 0;
        [SerializeField]
        float foodPerSheep = 10;
        [SerializeField]
        int maxSheepCount = 100;

        public readonly List<Sheep> sheepList = new List<Sheep>();
        public int sheepCount => sheepList.Count;

        protected void Start() {
            foreach (var sheep in GetComponentsInChildren<Sheep>()) {
                AddSheep(sheep);
            }
        }

        protected void FixedUpdate() {
            food += sheepList.Count * Time.deltaTime;
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
        }
    }
}
