using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToggleSetting
{
    public GameObject OnUI;
    public GameObject OffUI;
    public void ToggleHandle(bool IsOn)
    {
        if (IsOn)
        {
            OnUI.SetActive(true);
            OffUI.SetActive(false);
        }
        else
        {
            OnUI.SetActive(false);
            OffUI.SetActive(true);
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
        SoundSetting.ToggleHandle(SaveManager.Instance.OnSound);
        VibrationSetting.ToggleHandle(SaveManager.Instance.OnVibration);
    }
    public void ToHome()
    {
        PlaySoundClickBtn();
        LevelManager.Instance.ClearLevel();
        GameManager.Instance.OpenMainMenu();
        this.CloseDirectly();
    }

    public void ToContinues()
    {
        PlaySoundClickBtn();
        GameManager.Instance.ChangeStateGamePlay();
        this.CloseDirectly();
    }

    public void SoundHandle()
    {
        PlaySoundClickBtn();
        SoundManager.Instance.MuteHandle();
        SoundSetting.ToggleHandle(SaveManager.Instance.OnSound);
    }


    public void VibrationHandle()
    {
        PlaySoundClickBtn();
        bool IsVibration = SaveManager.Instance.OnVibration;
        SaveManager.Instance.OnVibration = !IsVibration;
        VibrationSetting.ToggleHandle(SaveManager.Instance.OnVibration);
    }
}
