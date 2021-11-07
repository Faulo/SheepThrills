using System;
using System.Collections;
using System.Collections.Generic;
using TheSheepGame.WorldObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {
    [SerializeField] private FoodDestructible _destructible;


    private void OnEnable() {
        _destructible.onObjectDestroyed += RunCredits;
    }

    private void OnDisable() {
        _destructible.onObjectDestroyed -= RunCredits;
    }


    private void RunCredits() {
        SceneManager.LoadScene("EndCutscene");
    }
}
