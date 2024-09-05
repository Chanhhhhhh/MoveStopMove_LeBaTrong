using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState {MainMenu, GamePlay, Setting, Win, Lose, ShopWeapon, ShopSkin }
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
            case GameState.MainMenu:
                UIManager.Instance.OpenUI<MainMenu>(); 
                break;
            case GameState.ShopWeapon:
                UIManager.Instance.OpenUI<ShopWeapon>();
                break;
            case GameState.ShopSkin:
                UIManager.Instance.OpenUI<ShopSkin>();
                break;
            case GameState.GamePlay:
                UIManager.Instance.OpenUI<GamePlay>();
                LevelManager.Instance.OnInit();
                break;
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
