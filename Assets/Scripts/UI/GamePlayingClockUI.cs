using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    private bool isPlaying = false;

    private void Update() {
       if(isPlaying) timerImage.fillAmount = GameManager.Instance.GetPlayingTimerNormalized();
    }

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e) {
        isPlaying = GameManager.Instance.IsGamePlaying();   
    }

}
