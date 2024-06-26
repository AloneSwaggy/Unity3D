using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public event EventHandler OnGameOver;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    // [SerializeField] private Player player;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private GameInput1 gameInput1;
    [SerializeField] private GameInput2 gameInput2;



    private State state;

    private float waitingToStartTimer = 1;
    private float countDownToStartTimer = 3;
    private float gamePlayingTimer = 90;
    private float gamePlayingTimeTotal;
    private bool isGamePause = false;

    void Awake()
    {
        Instance = this;
        gamePlayingTimeTotal = gamePlayingTimer;
    }
    private void Start()
    {
        // 确保在 Unity 编辑器中给 player1, player2, gameInput1 和 gameInput2 分配了正确的引用
        if (player1 == null || player2 == null || gameInput1 == null || gameInput2 == null)
        {
            Debug.LogError("Player or GameInput references not assigned in GameManager.");
            return;
        }
        gameInput1.Initialize();
        gameInput2.Initialize();
        // 初始化玩家和输入
        player1.Initialize(gameInput1);
        player2.Initialize(gameInput2);

        TurnToWaitingToStart();
        // GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        // GameInput1.Instance.OnPauseAction += GameInput_OnPauseAction;
        gameInput1.OnPauseAction += GameInput_OnPauseAction;
        gameInput2.OnPauseAction += GameInput_OnPauseAction;
        // GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        // GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        gameInput1.OnInteractAction += GameInput_OnInteractAction;
        gameInput2.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                // waitingToStartTimer -= Time.deltaTime;
                // if (waitingToStartTimer <= 0)
                // {
                //     TurnToCountDownToStart();
                // }
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0)
                {
                    TurnToGamePlaying();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    TurnToGameOver();
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }

    private void TurnToWaitingToStart()
    {
        state = State.WaitingToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToGameOver()
    {
        state = State.GameOver;
        DisablePlayer();

        OnGameOver?.Invoke(this, EventArgs.Empty);

        OnStateChanged?.Invoke(this, EventArgs.Empty);

    }

    private void DisablePlayer()
    {
        player1.enabled = false;
        player2.enabled = false;
    }
    private void EnablePlayer()
    {
        player1.enabled = true;
        player2.enabled = true;
    }
    public bool IsWaitingToStartState()
    {
        return state == State.WaitingToStart;
    }
    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }
    public bool IsGamePlayingState()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOverState()
    {
        return state == State.GameOver;
    }
    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }
    public void ToggleGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Debug.Log("Game Paused");
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimeTotal;
    }
}

