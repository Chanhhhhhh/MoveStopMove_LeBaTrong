using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBase : UICanvas
{
    public void OnExit()
    {
        this.CloseDirectly();
        GameManager.ChangeState(GameState.MainMenu);
    }
}
