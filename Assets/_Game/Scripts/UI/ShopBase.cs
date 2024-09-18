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
        PlaySoundClickBtn();
        GameManager.ChangeState(GameState.MainMenu);
    }

    public virtual void SetStateItem(int state, int price)
    {
        StateBtn.gameObject.SetActive(false);
        BuyBtn.gameObject.SetActive(false);
        switch (state)
        {
            case 0:
                StateBtn.gameObject.SetActive(true);
                State.text = Constant.STRING_EQUIPED;
                break;
            case 1:
                StateBtn.gameObject.SetActive(true);
                State.text = Constant.STRING_SELECT;
                break;
            case 2:
                BuyBtn.gameObject.SetActive(true);
                Price.text = price.ToString();
                break;
            default:
                break;
        }
    }
}
