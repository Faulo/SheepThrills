using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace TheSheepGame
{
    public class Credits : MonoBehaviour {
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private GameObject _credits;
        [SerializeField] private GameObject _buttons;


        private IEnumerator Start() {
            yield return new WaitForSeconds(2);
            while (_videoPlayer.isPlaying) {
                yield return new WaitForEndOfFrame();
            }
            _credits.SetActive(true);
            yield return new WaitForSeconds(10);
            _credits.SetActive(false);
            _buttons.SetActive(true);
        }


        public void OnHandleRestart() {
            SceneManager.LoadScene("Level_1");
        }

        public void OnHandleQuit() {
            Application.Quit();
        }
    }
}
