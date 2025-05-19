using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.IsGameOver()) {
            recipeDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString(); 
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
