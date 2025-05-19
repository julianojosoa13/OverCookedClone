using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
   
   private void Start() {
      GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
      GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

      Hide();
   }

   private void GameManager_OnGamePaused(object sender, EventArgs e) {
      Show();
   }
   
    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
      Hide();
   }

    public void GoToMainMenu() {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

   private void Show() {
    gameObject.SetActive(true);
   }


   private void Hide() {
    gameObject.SetActive(false);
   }
}
