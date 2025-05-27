using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameInput gameInput;

    private enum State
    {
        WaitingToStart,
        CoundownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private bool isGamePaused = false;

    private float waitingToStartTimer = 1f;
    private float coundownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 120f;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        gameInput.OnPause += GameInput_OnPause;
    }

    private void GameInput_OnPause(object sender, EventArgs e)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1f;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 0f;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        isGamePaused = !isGamePaused;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    state = State.CoundownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CoundownToStart:
                coundownToStartTimer -= Time.deltaTime;
                if (coundownToStartTimer <= 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CoundownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return coundownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
}
