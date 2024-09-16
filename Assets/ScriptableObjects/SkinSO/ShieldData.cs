using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shield
{
    public GameObject ShieldPrefabs;
    public Sprite Image;
}

[CreateAssetMenu(fileName = "ShieldData", menuName = "ScriptableObjects/ShieldData", order = 4)]
public class ShieldData : SkinData
{
    public Shield[] ShieldDatas;

}
