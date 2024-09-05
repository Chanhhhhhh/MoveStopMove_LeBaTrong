using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void OnPlay()
    {
        GameManager.ChangeState(GameState.GamePlay);
    }

    public void OpenShopWeapon()
    {
        GameManager.ChangeState(GameState.ShopWeapon);
    }

    public void OpenShopSkin()
    {

    }
}
