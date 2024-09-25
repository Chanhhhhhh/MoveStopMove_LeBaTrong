using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI textCountDown;
    [SerializeField] private GameObject ImageLoading;
    [SerializeField] private Button ReviveBtn;

    private float SpeedLoading = -200f;
    private float CountDownRevive;
    private float Count;

    private void Start()
    {
        ReviveBtn.onClick.AddListener(() => OnRevive());
    }
    public override void Setup()
    {
        base.Setup();
        CountDownRevive = 4.9f;
        Count = Mathf.CeilToInt(CountDownRevive);
        SoundManager.Instance.PlaySoundClip(SoundType.ReviveCount);
        if (SaveManager.Instance.Coin < LevelManager.Instance.PriceRevive)
        {
            ReviveBtn.interactable = false;
        }
        ReviveBtn.interactable = true;
    }
    private void Update()
    {       
        if (GameManager.IsState(GameState.PopUpRevive))
        {
            ImageLoading.transform.Rotate(0f, 0f, SpeedLoading * Time.deltaTime, Space.Self);
            CountDownRevive -= Time.deltaTime;
            textCountDown.text = Mathf.CeilToInt(CountDownRevive).ToString();
            if (Mathf.CeilToInt(CountDownRevive) < Count)
            {
                SoundManager.Instance.PlaySoundClip(SoundType.ReviveCount);
                Count = Mathf.CeilToInt(CountDownRevive);
            }
            if (CountDownRevive <= -0.5f)
            {                
                OnExit();
            }
        }
    }

    public void OnExit()
    {
        LevelManager.Instance.SetLose();
        this.CloseDirectly();
    }
    public void OnRevive()
    {          
        LevelManager.Instance.Revive();
        this.CloseDirectly();
    }
}
