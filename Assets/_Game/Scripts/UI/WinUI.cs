using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : ResultBaseUI
{
    
    public void PlayNextZone()
    {
        PlaySoundClickBtn();
        LevelManager.Instance.ClearLevel();
        GameManager.ChangeState(GameState.MainMenu);
    }
}
