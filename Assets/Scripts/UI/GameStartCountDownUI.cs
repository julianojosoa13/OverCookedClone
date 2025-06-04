using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string ANIMATION_POPUP_NUMBER_TRIGGER = "NumberPopUp";
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;

    private int previousCountdownNumber;

    private void Awake() {
       animator = GetComponent<Animator>();
    }

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

        Hide();
    }

    private void Update() {
        int countDownNumber =  Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
        countdownText.text = countDownNumber.ToString();

        if(previousCountdownNumber != countDownNumber) {
            previousCountdownNumber = countDownNumber;
            animator.SetTrigger(ANIMATION_POPUP_NUMBER_TRIGGER);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.IsCountdownToStartActive()) {
            Show();
        } else {
            Hide();
        }
    }


    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
