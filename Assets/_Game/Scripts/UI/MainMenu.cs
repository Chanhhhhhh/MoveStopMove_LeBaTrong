using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void OnPlay()
    {
        LevelManager.Instance.OnInit();
        GameManager.ChangeState(GameState.GamePlay);
        this.CloseDirectly();
    }

    public void OpenShopWeapon()
    {
        GameManager.ChangeState(GameState.ShopWeapon);
        this.CloseDirectly() ;
    }

    public void OpenShopSkin()
    {
        GameManager.ChangeState(GameState.ShopSkin);
        this.CloseDirectly() ;
    }
}
