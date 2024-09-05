using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : ShopBase
{
    [SerializeField] private TextMeshProUGUI NameWeapon;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI State;
    [SerializeField] private TextMeshProUGUI Price;

    [SerializeField] private Button StateBtn;
    [SerializeField] private Button BuyBtn;

    [SerializeField] private Transform WeaponObj;

    private int Index;
    private int AmountWeapon;
    private void Start()
    {
        Setup();
    }
    public override void Setup()
    {
        base.Setup();
        Index = 0;
        AmountWeapon = DataManager.Instance.weapons.Count;
        ShowWeapon(Index);
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
        }

    }
    public void NextWeapon()
    {
        Index++;
        if(Index >= AmountWeapon)
        {
            Index = 0;
        }
        ShowWeapon(Index);

    }

    public void PrevWeapon()
    {
        Index--;
        if(Index < 0)
        {
            Index = AmountWeapon - 1;
        }
        ShowWeapon(Index);
    }

    public void StateWeapon()
    {

    }
    public void BuyWeapon()
    {

    }
}
