using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private readonly List<PoolType> weaponTypes = new List<PoolType>() {PoolType.Candy, PoolType.Arrow, PoolType.Axe, PoolType.Knife, PoolType.Hammer };
    [SerializeField] private ColorData colorData;
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
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
