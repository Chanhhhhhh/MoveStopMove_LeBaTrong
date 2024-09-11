using System.IO;
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


    private void Awake()
    {
        LoadData();
    }
    public void SaveData()
    {
        GameData saveData = new GameData
        {
            coin = this.Coin,

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
            coin = 0,
        };
        if (!File.Exists(path))
        {
            Debug.Log("Cann't load data, file not found");
            this.coin = defaultData.coin;
            SaveData();
            return;
        }
        string json = File.ReadAllText(path);
        defaultData = JsonUtility.FromJson<GameData>(json);
        this.coin = defaultData.coin;

    }

}


public class GameData
{
    public int coin;
}


