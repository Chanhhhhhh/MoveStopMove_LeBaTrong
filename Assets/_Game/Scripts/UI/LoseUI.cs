using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI textRank;
    [SerializeField] private TextMeshProUGUI textKiller;
    public void SetResult( int rank, string killer)
    {
        textRank.text = "#" + rank.ToString();
        textKiller.text = killer;
    }
    public void TouchToContinue()
    {
        LevelManager.Instance.ClearLevel();
        GameManager.ChangeState(GameState.MainMenu);
       
    }
}
