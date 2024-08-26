using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PoolType weaponType;
    public WeaponData weaponData;
    [SerializeField] GameObject Child;

    public void OnChild()
    {
        Child.SetActive(true);
    }

    public void OffChild()
    {
        Child?.SetActive(false);
    }
}
