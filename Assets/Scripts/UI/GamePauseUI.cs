using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
   [SerializeField] private Button optionsButton;
   [SerializeField] private Button resumeButton;
   [SerializeField] private Button mainMenuButton;
   void Awake()
   {
      gameObject.SetActive(true);

      optionsButton.onClick.AddListener(() =>
      {
         OptionsUI.Instance.Show(Show);
         Hide();
      });

      resumeButton.onClick.AddListener(() =>
      {
         GameManager.Instance.TogglePause();
         Hide();
      });
   }

   private void Start()
   {
      GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
      GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

      Hide();
   }

   private void GameManager_OnGamePaused(object sender, EventArgs e)
   {
      Show();
   }

   private void GameManager_OnGameUnpaused(object sender, EventArgs e)
   {
      Hide();


   }

   public void GoToMainMenu()
   {
      Loader.Load(Loader.Scene.MainMenuScene);
   }

   private void Show()
   {
      gameObject.SetActive(true);

      resumeButton.Select();
   }


   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
