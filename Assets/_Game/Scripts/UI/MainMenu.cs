using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainMenu : UICanvas
{
    [SerializeField] private ToggleSetting SoundMainMenu;
    [SerializeField] private ToggleSetting VibrationMainMenu;
    [SerializeField] private TMP_InputField NamePlayer;
    [SerializeField] private TextMeshProUGUI BestZoneScore;

    [SerializeField] private RectTransform ContentLeft;
    [SerializeField] private RectTransform ContentRight;

    [SerializeField] private float OpenDelayTime = 0.3f;
    [SerializeField] private float CloseDelayTime = 0.3f;

    private Sequence sequence;

    private void Start()
    {
        NamePlayer.onValueChanged.AddListener(LevelManager.Instance.SetNamePlayer);
    }  
    public override void Setup()
    {
        base.Setup();
        NamePlayer.text = SaveManager.Instance.NamePlayer;
        SoundMainMenu.ToggleHandle(SaveManager.Instance.OnSound);
        VibrationMainMenu.ToggleHandle(SaveManager.Instance.OnVibration);
        BestZoneScore.text = "ZONE:" + (SaveManager.Instance.Zone + 1).ToString() + "  -  BEST:" + LevelManager.Instance.BestRank.ToString();
    }
    public void OnPlay()
    {
        PlaySoundClickBtn();
        UIManager.Instance.CloseUI<CoinUI>();
        GameManager.Instance.ChangeStateGamePlay();
        LevelManager.Instance.OnInit();
        this.CloseDirectly();
    }

    public void OpenShopWeapon()
    {
        PlaySoundClickBtn(); 
        GameManager.ChangeState(GameState.ShopWeapon);
        UIManager.Instance.OpenUI<ShopWeapon>();
        this.CloseDirectly();
    }

    public void OpenShopSkin()
    {
        PlaySoundClickBtn();
        GameManager.ChangeState(GameState.ShopSkin);
        UIManager.Instance.OpenUI<ShopSkin>();
        this.CloseDirectly();
    }

    public void SoundHandle()
    {
        PlaySoundClickBtn();
        SoundManager.Instance.MuteHandle();
        SoundMainMenu.ToggleHandle(SaveManager.Instance.OnSound);
    }


    public void VibrationHandle()
    {
        PlaySoundClickBtn();
        bool IsVibration = SaveManager.Instance.OnVibration;
        SaveManager.Instance.OnVibration = !IsVibration;
        VibrationMainMenu.ToggleHandle(SaveManager.Instance.OnVibration);
    }

    public override void Open()
    {
        base.Open();
        ContentLeft.DOAnchorMax(new Vector2(0, 0.5f), OpenDelayTime).From(new Vector2(-1, 0.5f));
        ContentRight.DOAnchorMax(new Vector2(1, 0.5f), OpenDelayTime).From(new Vector2(2, 0.5f));
        sequence?.Kill();
    }

    public override void CloseDirectly()
    {
        sequence.Join(ContentLeft.DOAnchorMax(new Vector2(-1, 0.5f), CloseDelayTime).From(new Vector2(0, 0.5f)));
        sequence.Join(ContentRight.DOAnchorMax(new Vector2(2, 0.5f), CloseDelayTime).From(new Vector2(1, 0.5f))
            .OnComplete(() =>
            {
                base.CloseDirectly();
            }));
    }
}
