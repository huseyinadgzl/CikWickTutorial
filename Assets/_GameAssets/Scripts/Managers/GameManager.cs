using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int OnGameStateChanged { get; internal set; }

    public event Action<GameState> OnGameStateChange;
    [Header("Preferences")]
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;

    private GameState _currentGameState;
    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChange?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State: " + gameState);
    }

    public void OnEggCollected()
    {
        _currentEggCount++;

        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            //WİNN
            Debug.Log("Game Wİnnn?Ğ=??==?=");
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);

        }
        Debug.Log("Egg Collected : " + _currentEggCount);
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
