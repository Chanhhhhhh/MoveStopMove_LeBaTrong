using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private readonly List<PoolType> weaponTypes = new List<PoolType>() {PoolType.Candy, PoolType.Arrow, PoolType.Axe, PoolType.Knife, PoolType.Hammer };
    public ColorData colorData;
    public HatData hatData;
    public PantData pantData;
    public ShieldData shieldData;
    public Dictionary<ShopState, SkinData> skinDataDic = new Dictionary<ShopState, SkinData>();
    public List<Weapon> weapons;
    

    public void OnInit()
    {
        skinDataDic.Add(ShopState.HatShop, hatData);
        skinDataDic.Add(ShopState.PantShop, pantData);
        skinDataDic.Add(ShopState.ShieldShop, shieldData);
    }
    public Material GetColor()
    {
        int color = Random.Range(0, colorData.colorMats.Length);
        return colorData.colorMats[color];
    }


    public PoolType RandomWeapon()
    {
        return weaponTypes[Random.Range(0, weapons.Count)];
    }
    public Weapon GetWeapon(PoolType weaponType)
    {
        for(int i = 0;  i < weapons.Count; i++)
        {
            if (weapons[i].weaponType == weaponType)
            {
                return Instantiate(weapons[i]);
            }
        }
        return null;
    }
}
