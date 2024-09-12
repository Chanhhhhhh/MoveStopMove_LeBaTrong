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
        SaveManager.Instance.LoadData();
        ChangeState(GameState.MainMenu);
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.MainMenu:  
                UIManager.Instance.CloseAll();
                UIManager.Instance.OpenUI<CoinUI>();
                UIManager.Instance.OpenUI<MainMenu>(); 
                break;
            case GameState.ShopWeapon:
                UIManager.Instance.OpenUI<ShopWeapon>();
                break;
            case GameState.ShopSkin:
                UIManager.Instance.OpenUI<ShopSkin>();
                break;
            case GameState.GamePlay:
                UIManager.Instance.CloseUI<CoinUI>();
                UIManager.Instance.OpenUI<GamePlay>();
                break;
            case GameState.Setting:
                UIManager.Instance.OpenUI<SettingUI>();
                break;
            case GameState.Win:
                UIManager.Instance.CloseAll();
                UIManager.Instance.OpenUI<WinUI>();
                break;
            case GameState.Lose:
                UIManager.Instance.CloseAll();
                UIManager.Instance.OpenUI<LoseUI>();
                break;
            default:
                break;
        }
        Camerafollow.Instance.SwitchCamera(gameState);
        OnGameStateChange?.Invoke(state);
    }
    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
}
