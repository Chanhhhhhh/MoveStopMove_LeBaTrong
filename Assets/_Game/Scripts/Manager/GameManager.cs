using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState {MainMenu, GamePlay, Setting, Win, Lose }
public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    public static UnityEvent<GameState> OnGameStateChange;
    private void Awake()
    {
        Application.targetFrameRate= 60;
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            default:
                break;
        }

        OnGameStateChange?.Invoke(state);
    }
    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
}
