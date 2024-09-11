using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI textAlive;
    public Transform TargetIndicatorContent;
    
    public void OnSetting()
    {
        GameManager.ChangeState(GameState.Setting);
    }

    public void SetAlive(int amount)
    {
        textAlive.text = "Alive : " + amount.ToString();
    }
}
