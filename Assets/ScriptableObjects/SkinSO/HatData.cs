using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Hat
{
    public GameObject HatPrefabs;
    public Sprite Image;
}

[CreateAssetMenu(fileName = "HatData", menuName = "ScriptableObjects/HatData", order = 2)]
public class HatData : SkinData
{
    public Hat[] hatDatas;
}
