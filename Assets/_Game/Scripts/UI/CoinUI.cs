using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI TextCoin;
    [SerializeField] private RectTransform CoinTF;

    private Tween tween;
    public override void Setup() 
    {
        base.Setup();
        SetCoin(SaveManager.Instance.Coin);
    }
    public void SetCoin(int coin)
    {
        TextCoin.text = coin.ToString();
    }

    public override void Open()
    {
        base.Open();
        CoinTF.anchoredPosition = new Vector2(100f, CoinTF.anchoredPosition.y);
        tween?.Kill();
    }
    public override void CloseDirectly()
    {
        tween = CoinTF.DOAnchorPosX(1000f, 0.3f).From(new Vector2(100f, CoinTF.anchoredPosition.y)).OnComplete(() =>
        {
            base.CloseDirectly();
        });
    }
}
