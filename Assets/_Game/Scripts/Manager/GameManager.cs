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
        LevelManager.Instance.CreateZone(SaveManager.Instance.Zone);
        SoundManager.Instance.OnInit();
        LevelManager.Instance.player.GetSaveItem();        
        OpenMainMenu();
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;        
        Camerafollow.Instance.SwitchCamera(gameState);
        OnGameStateChange?.Invoke(state);
    }
    public static bool IsState(GameState state)
    {
        return gameState == state;
    }

    public void OpenMainMenu()
    {
        ChangeState(GameState.MainMenu);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CoinUI>();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    public void ChangeStateGamePlay()
    {
        ChangeState(GameState.GamePlay);
        UIManager.Instance.OpenUI<GamePlay>(1f);
    }
}
