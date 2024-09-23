using TMPro;
using UnityEngine;

public class LoseUI : ResultBaseUI
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
        PlaySoundClickBtn();
        LevelManager.Instance.ClearLevel();
        GameManager.ChangeState(GameState.MainMenu);       
    }




}
