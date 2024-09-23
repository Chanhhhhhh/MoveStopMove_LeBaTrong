using DG.Tweening;
using TMPro;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI textAlive;
    [SerializeField] private RectTransform Setting;
    [SerializeField] private RectTransform Alive;

    private Sequence sequence;

    public override void Open()
    {
        float SettingY = Setting.anchoredPosition.y;
        float AliveY = Alive.anchoredPosition.y;
        base.Open();
        Setting.DOAnchorPos(new Vector2(-50f, SettingY), 0.3f).From(new Vector2(1000f, SettingY));
        Alive.DOAnchorPos(new Vector2(-100f, AliveY), 0.3f).From(new Vector2(-1000f, AliveY));
        sequence?.Kill();
        
    }
    public void OnSetting()
    {
        PlaySoundClickBtn();
        GameManager.ChangeState(GameState.Setting);
        CloseDirectly();
        
    }

    public void SetAlive(int amount)
    {
        textAlive.text = "Alive : " + amount.ToString();
    }
    public override void CloseDirectly()
    {
        float SettingY = Setting.anchoredPosition.y;
        float AliveY = Alive.anchoredPosition.y;
        sequence.Join(Setting.DOAnchorPos(new Vector2(1000f, SettingY), 0.3f).From(new Vector2(-50f, SettingY)));
        sequence.Join(Alive.DOAnchorPos(new Vector2(-1000f, AliveY), 0.3f).From(new Vector2(-100f, AliveY)).OnComplete(() =>
        {
            base.CloseDirectly();
        }));
    }
}
