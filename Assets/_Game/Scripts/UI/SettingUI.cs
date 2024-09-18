using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToggleSetting
{
    public GameObject OnUI;
    public GameObject OffUI;
    public void ToggleHandle(bool IsOff)
    {
        if (IsOff)
        {
            OnUI.SetActive(false);
            OffUI.SetActive(true);
        }
        else
        {
            OnUI.SetActive(true);
            OffUI.SetActive(false);
        }
    }
}
public class SettingUI : UICanvas
{
    [SerializeField] private ToggleSetting SoundSetting;
    [SerializeField] private ToggleSetting VibrationSetting;
    public override void Setup()
    {
        base.Setup();
    }
    public void ToHome()
    {
        PlaySoundClickBtn();
        LevelManager.Instance.ClearLevel();
        GameManager.ChangeState(GameState.MainMenu);
        this.CloseDirectly();
    }

    public void ToContinues()
    {
        PlaySoundClickBtn();
        GameManager.ChangeState(GameState.GamePlay);
        this.CloseDirectly();
    }

    public void SoundHandle()
    {
        PlaySoundClickBtn();
        SoundManager.Instance.MuteHandle();
        SoundSetting.ToggleHandle(SoundManager.Instance.IsMute);
    }


    public void VibrationHandle()
    {

    }
}
