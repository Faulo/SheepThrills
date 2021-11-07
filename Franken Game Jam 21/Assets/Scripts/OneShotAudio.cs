using System;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Audio;

namespace TheSheepGame {
    public class OneShotAudio : MonoBehaviour {
        [SerializeField]
        AudioClip[] clips = Array.Empty<AudioClip>();
        [SerializeField, Expandable]
        AudioMixerGroup mixer = default;
        [SerializeField, Range(0, 10)]
        float pitchMininum = 1;
        [SerializeField, Range(0, 10)]
        float pitchMaximum = 1;

        AudioSource audioSource;
        protected void Awake() {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.pitch = UnityEngine.Random.Range(pitchMininum, pitchMaximum);
            audioSource.clip = clips.RandomElement();
            audioSource.outputAudioMixerGroup = mixer;
        }
        protected void Start() {
            audioSource.Play();
        }
        protected void Update() {
            if (!audioSource.isPlaying) {
                Destroy(gameObject);
            }
        }
        public void InstantiatePrefab() {
            Instantiate(this);
        }
    }
}
