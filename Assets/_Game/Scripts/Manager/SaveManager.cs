using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
 
public class SaveManager : Singleton<SaveManager>
{
    private const string PATH = "/savegame.json";

    private int coin;
    public int Coin
    {
        get { return coin; }
        set
        {
            coin = value;
            SaveData();
        }
    }
    #region : ListItem

    private SaveItemBase weaponStates = new SaveItemBase();
    public SaveItemBase WeaponStates { get { return weaponStates; } }

    private SaveItemBase hatStates = new SaveItemBase();
    public SaveItemBase HatStates { get { return hatStates; } }

    private SaveItemBase pantStates = new SaveItemBase();
    public SaveItemBase PantStates { get { return pantStates; } }

    private SaveItemBase shieldStates = new SaveItemBase();
    public SaveItemBase ShieldStates { get { return shieldStates; } }
    #endregion

    private bool onSound;
    public bool OnSound
    {
        get { return onSound; }
        set
        {
            onSound = value;
            SaveData();
        }
    }

    private bool onVibration;
    public bool OnVibration
    {
        get { return onVibration; }
        set
        {
            onVibration = value;
            SaveData();
        }
    }

    private string namePlayer;
    public string NamePlayer
    {
        get { return namePlayer; }
        set
        {
            namePlayer = value;
            SaveData();
        }
    }

    private int zone;
    public int Zone
    {
        get { return zone;}
        set
        {
            zone = value;
            SaveData();
        }
    }

    private int bestRank;
    public int BestRank
    {
        get { return bestRank; }
        set
        {
            bestRank = value;
            SaveData();
        }
    }
    public void SaveData()
    {
        GameData saveData = new GameData
        {
            coin = this.coin,
            WeaponState = this.weaponStates,
            HatState = this.hatStates,
            PantState = this.pantStates,
            ShieldState = this.shieldStates,
            OnSound = this.onSound,
            OnVibration = this.onVibration,
            NamePlayer = this.namePlayer,
            Zone = this.zone,
            BestRank = this.bestRank,
        };
        string path = Application.persistentDataPath + PATH;
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + PATH;
        GameData defaultData = new GameData
        {
            coin = 10000,
            WeaponState = new SaveItemBase { currentItem = 0, ItemStates = CreateListDefault(DataManager.Instance.weapons.Count) },
            HatState = new SaveItemBase { currentItem = -1, ItemStates = CreateListDefault(DataManager.Instance.hatData.hatDatas.Length) },
            PantState = new SaveItemBase { currentItem = -1, ItemStates = CreateListDefault(DataManager.Instance.pantData.PantDatas.Length) },
            ShieldState = new SaveItemBase { currentItem = -1, ItemStates = CreateListDefault(DataManager.Instance.shieldData.ShieldDatas.Length) },
            OnSound = true,
            OnVibration = true,
            NamePlayer = "You",
            Zone = 0,
            BestRank = LevelManager.Instance.ReturnFirstBestRank(),
        };
        defaultData.WeaponState.ItemStates[0] = 0;
        if (!File.Exists(path))
        {
            Debug.Log("Cann't load data, file not found");
            SetData(defaultData);
            SaveData();
            return;
        }
        string json = File.ReadAllText(path);
        defaultData = JsonUtility.FromJson<GameData>(json);
        SetData(defaultData);

    }

    private void SetData(GameData data)
    {
        this.coin = data.coin;
        this.weaponStates = data.WeaponState;
        this.hatStates = data.HatState;
        this.pantStates = data.PantState;
        this.shieldStates = data.ShieldState;
        this.onSound = data.OnSound;
        this.onVibration = data.OnVibration;
        this.namePlayer = data.NamePlayer;
        this.zone = data.Zone;
        this.bestRank = data.BestRank;
    }
    public List<int> CreateListDefault(int count)
    {
        List<int> list = new List<int>();

        for (int i = 0; i < count; i++)
        {
            list.Add(2);
        }
        return list;
    }
    public SaveItemBase GetSaveItemSkin(ShopState shopState)
    {
        switch (shopState)
        {
            case ShopState.HatShop:
                return HatStates;
            case ShopState.PantShop:
                return PantStates;
            case ShopState.ShieldShop:
                return ShieldStates;
            default:
                return null;
        }
    }

}

[System.Serializable]
public class  SaveItemBase
{ 
    public int currentItem;
    public List<int> ItemStates;
    public void SaveItem(int index)
    {
        if(currentItem >= 0)
        {
            ItemStates[currentItem] = 1;
        }       
        ItemStates[index] = 0;
        currentItem = index;
        SaveManager.Instance.SaveData();
    }
}
public class GameData
{
    public int coin;
    public SaveItemBase WeaponState;
    public SaveItemBase HatState;
    public SaveItemBase PantState;
    public SaveItemBase ShieldState;
    public bool OnSound;
    public bool OnVibration;
    public string NamePlayer;
    public int Zone;
    public int BestRank;
}


