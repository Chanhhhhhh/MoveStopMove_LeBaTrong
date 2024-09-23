using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultBaseUI : UICanvas
{
    [SerializeField] private GameObject BtnContinues;
    [SerializeField] private TextMeshProUGUI textCoinBonus;
    private void OnEnable()
    {
        StartCoroutine(ActiveBtnCoroutine());
    }
    public override void Setup()
    {
        base.Setup();
        BtnContinues.SetActive(false);
    }
    public void SetTextCoinBonus(int amount)
    {
        textCoinBonus.text = amount.ToString();
    }
    IEnumerator ActiveBtnCoroutine()
    {
        yield return new WaitForSeconds(2f);
        BtnContinues.SetActive(true);
    }

}
