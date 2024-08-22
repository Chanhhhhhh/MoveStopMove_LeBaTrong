using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    public Material GetColor()
    {
        int color = Random.Range(0, colorData.colorMats.Length);
        return colorData.colorMats[color];
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
