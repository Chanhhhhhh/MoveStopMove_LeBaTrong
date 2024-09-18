using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBaseUI : UICanvas
{
    [SerializeField] private GameObject BtnContinues;
    private void OnEnable()
    {
        StartCoroutine(ActiveBtnCoroutine());
    }
    public override void Setup()
    {
        base.Setup();
        BtnContinues.SetActive(false);
    }
    IEnumerator ActiveBtnCoroutine()
    {
        yield return new WaitForSeconds(2f);
        BtnContinues.SetActive(true);
    }
}
