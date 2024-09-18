using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    [SerializeField] private ToggleSetting SoundMainMenu;
    [SerializeField] private ToggleSetting VibrationMainMenu;
    public void OnPlay()
    {
        PlaySoundClickBtn();
        LevelManager.Instance.OnInit();
        GameManager.ChangeState(GameState.GamePlay);
        this.CloseDirectly();
    }

    public void OpenShopWeapon()
    {
        PlaySoundClickBtn();
        GameManager.ChangeState(GameState.ShopWeapon);
        this.CloseDirectly() ;
    }

    public void OpenShopSkin()
    {
        PlaySoundClickBtn();
        GameManager.ChangeState(GameState.ShopSkin);
        this.CloseDirectly() ;
    }

    public void SoundHandle()
    {
        PlaySoundClickBtn();
        SoundManager.Instance.MuteHandle();
        SoundMainMenu.ToggleHandle(SoundManager.Instance.IsMute);
    }


    public void VibrationHandle()
    {

    }
}
