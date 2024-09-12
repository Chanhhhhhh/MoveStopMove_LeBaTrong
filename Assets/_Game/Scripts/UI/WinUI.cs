using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : UICanvas
{


    public void PlayNextZone()
    {
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Instance.ClearLevel();
    }
}
