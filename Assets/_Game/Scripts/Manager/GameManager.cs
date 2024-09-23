using UnityEngine;
using UnityEngine.Events;

public enum GameState {MainMenu, GamePlay, Setting, Win, Lose, ShopWeapon, ShopSkin, PopUpRevive }
public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    public static UnityEvent<GameState> OnGameStateChange;
    private void Awake()
    {
        Application.targetFrameRate= 60;
        SaveManager.Instance.LoadData();
        DataManager.Instance.OnInit();
        SoundManager.Instance.OnInit();
        LevelManager.Instance.player.GetSaveItem();
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
                UIManager.Instance.OpenUI<GamePlay>(1f);
                break;
            case GameState.Setting:
                UIManager.Instance.OpenUI<SettingUI>();
                break;
            case GameState.PopUpRevive:
                UIManager.Instance.CloseAll();
                UIManager.Instance.OpenUI<ReviveUI>();
                break;
            case GameState.Win:
                UIManager.Instance.CloseAll();
                UIManager.Instance.OpenUI<WinUI>();
                break;
            case GameState.Lose:
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
