using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI TextCoin;

    public override void Setup() 
    {
        base.Setup();
        SetCoin(SaveManager.Instance.Coin);
    }
    public void SetCoin(int coin)
    {
        TextCoin.text = coin.ToString();
    }
}
