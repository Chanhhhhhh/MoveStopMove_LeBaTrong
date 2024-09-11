using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : UICanvas
{
    public void ToHome()
    {
        LevelManager.Instance.ClearLevel();
        GameManager.ChangeState(GameState.MainMenu);
        this.CloseDirectly();
    }

    public void ToContinues()
    {
        GameManager.ChangeState(GameState.GamePlay);
        this.CloseDirectly();
    }
}
