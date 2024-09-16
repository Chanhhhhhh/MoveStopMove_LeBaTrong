using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : UICanvas
{


    public void PlayNextZone()
    {
        LevelManager.Instance.ClearLevel();
        GameManager.ChangeState(GameState.MainMenu);
    }
}
