using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinUI : ResultBaseUI
{
    [SerializeField] private TextMeshProUGUI PlayZone;
    public override void Setup()
    {
        base.Setup();
        PlayZone.text = "Play Zone " + (SaveManager.Instance.Zone + 1).ToString();

    }
    public void PlayNextZone()
    {
        PlaySoundClickBtn();
        LevelManager.Instance.ClearLevel();
        LevelManager.Instance.CreateZone(SaveManager.Instance.Zone);
        GameManager.Instance.OpenMainMenu();
    }
}
