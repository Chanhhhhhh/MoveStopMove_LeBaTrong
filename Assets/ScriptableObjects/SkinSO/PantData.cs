using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class  Pant
{
    public Material PantMaterial;
    public Sprite Image;
}


[CreateAssetMenu(fileName = "PantData", menuName = "ScriptableObjects/PantData", order = 3)]
public class PantData : SkinData
{
    public Pant[] PantDatas;
} 
