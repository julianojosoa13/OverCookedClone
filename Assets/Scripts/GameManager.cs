using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public event EventHandler OnGameStateChanged;

   public static GameManager Instance {get; private set;}

   private enum State {
    WaitingToStart,
    CoundownToStart,
    GamePlaying,
    GameOver,
   }

   private State state;

   private float waitingToStartTimer = 1f;
   private float coundownToStartTimer = 3f;
   private float gamePlayingTimer = 10f;

   private void Awake() {
      Instance = this;
      state = State.WaitingToStart;
   }

   private void Update() {
      switch(state) {
        case State.WaitingToStart:
            waitingToStartTimer -= Time.deltaTime;
            if(waitingToStartTimer <= 0f) {
                state = State.CoundownToStart;
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
        case State.CoundownToStart:
            coundownToStartTimer -= Time.deltaTime;
            if(coundownToStartTimer <= 0f) {
                state = State.GamePlaying;
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
        case State.GamePlaying:
            gamePlayingTimer -= Time.deltaTime;
            if(gamePlayingTimer <= 0f) {
                state = State.GameOver;
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
        case State.GameOver:
            break;
      }
      Debug.Log(state);
   }

   public bool IsGamePlaying() {
    return state == State.GamePlaying; 
   }

   public bool IsCountdownToStartActive() {
    return state == State.CoundownToStart;
   }

   public float GetCountDownToStartTimer() {
    return coundownToStartTimer;
   }
}
