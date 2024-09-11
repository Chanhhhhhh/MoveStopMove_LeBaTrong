using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBase : UICanvas
{
    [SerializeField] protected TextMeshProUGUI Description;
    [SerializeField] protected TextMeshProUGUI State;
    [SerializeField] protected TextMeshProUGUI Price;

    [SerializeField] protected Button StateBtn;
    [SerializeField] protected Button BuyBtn;
    public virtual void OnExit()
    {      
        GameManager.ChangeState(GameState.MainMenu);
        this.CloseDirectly();
    }
}
