using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : ShopBase
{
    [SerializeField] private TextMeshProUGUI NameWeapon;
    [SerializeField] private Transform WeaponObj;

    private int IndexWeapon;
    private int AmountWeapon;
    public override void Setup()
    {
        base.Setup();
        IndexWeapon = SaveManager.Instance.WeaponStates.currentItem;
        AmountWeapon = DataManager.Instance.weapons.Count;
        ShowWeapon(IndexWeapon);
    }

    public void ShowWeapon(int index)
    {
        foreach (Transform child in WeaponObj.transform)
        {
            Destroy(child.gameObject);
        }
        Weapon weapon = Instantiate(DataManager.Instance.weapons[index]);
        weapon.transform.SetParent(WeaponObj.transform, false);
        WeaponData weaponData = weapon.weaponData;
        if (weaponData != null)
        {
            NameWeapon.text = weaponData.NameWeapon;
            Description.text = weaponData.Description;
            SetStateItem(SaveManager.Instance.WeaponStates.ItemStates[index], weaponData.Price);
        }

    }
    public void NextWeapon()
    {
        IndexWeapon++;
        if (IndexWeapon >= AmountWeapon)
        {
            IndexWeapon = 0;
        }
        ShowWeapon(IndexWeapon);

    }

    public void PrevWeapon()
    {
        IndexWeapon--;
        if (IndexWeapon < 0)
        {
            IndexWeapon = AmountWeapon - 1;
        }
        ShowWeapon(IndexWeapon);
    }

    public void SelectWeapon()
    {
        if(IndexWeapon == SaveManager.Instance.WeaponStates.currentItem)
        {
            return;
        }
        LevelManager.Instance.player.ChangeWeapon(IndexWeapon);
        SaveWeapon();
        GameManager.ChangeState(GameState.MainMenu);
        this.CloseDirectly();

    }
     
    public void BuyWeapon()
    {
        int price = DataManager.Instance.weapons[IndexWeapon].weaponData.Price;
        if (SaveManager.Instance.Coin < price)
        {
            return;
        }
        LevelManager.Instance.player.ChangeWeapon(IndexWeapon);
        SaveWeapon();
        UIManager.Instance.GetUI<CoinUI>().SetCoin(SaveManager.Instance.Coin -= price);
        StateBtn.gameObject.SetActive(true);
        BuyBtn.gameObject.SetActive(false);
        State.text = Constant.STRING_EQUIPED;
    }
    private void SaveWeapon()
    {
        SaveManager.Instance.WeaponStates.SaveItem(IndexWeapon);
    }
}
