using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ShopState
{
    HatShop = 1,
    PantShop = 2,
    ShieldShop = 3,
    FullSkinShop = 4,
}

[System.Serializable]
public class ShopType
{
    public ShopState shopState;
    public Button ChangeShopBtn;
    public GameObject ShopView;
    public Transform ShopContent;


    public void OpenShop()
    {
        ChangeShopBtn.image.enabled = false;
        ShopView.SetActive(true);
    }
    public void CloseShop()
    {
        ChangeShopBtn.image.enabled = true;
        ShopView.SetActive(false);
    }


} 
public class ShopSkin : ShopBase
{
    private Player player;
    private UnityAction<ShopType> BtnAction;
    [SerializeField] private ButtonAction buttonAction;
    [SerializeField] private List<ShopType> shopTypes;
    private ShopType currentShop;

    private GameObject HatSelected;
    private Material PantSelected;
    private GameObject ShieldSelected;
    private ButtonAction ChooseButtonItem;

    private List<ButtonAction> BtnHats = new List<ButtonAction>();
    private List<ButtonAction> BtnPants = new List<ButtonAction>();
    private List<ButtonAction> BtnShields = new List<ButtonAction>();

    private bool IsLoaded = false;
    private int indexSelected;

    private void Awake()
    {
        
        for(int i = 0; i < shopTypes.Count; i++)
        {
            int index = i;
            shopTypes[i].ChangeShopBtn.onClick.AddListener(() => OnClickShopBtn(shopTypes[index]));

        }
        BtnAction += OpenShopView;
    }

    public override void Setup()
    {
        player = LevelManager.Instance.player;
        indexSelected = -1;
        base.Setup();
        if (!IsLoaded)
        {
            LoadShop();           
        }
        OpenShopView(shopTypes[0]);
        
    }

    public override void OnExit()
    {
        ClearTryOnItem();
        base.OnExit();

    }
    private void OnClickShopBtn(ShopType shopType)
    {
        PlaySoundClickBtn();
        BtnAction?.Invoke(shopType);
    }
    private void OpenShopView(ShopType shopType)
    {
        indexSelected = -1;
        ClearTryOnItem();
        if(currentShop == shopType)
        {
            return;
        }
        if (currentShop != null)
        {
            currentShop.CloseShop();
        }
        currentShop = shopType;
        currentShop.OpenShop();
        TryOnFirstItem();
    }

    private void TryOnFirstItem()
    {       
        switch (currentShop.shopState)
        {
            case ShopState.HatShop:
                TryOnHat(0, BtnHats[0]);
                break;
            case ShopState.PantShop:
                TryOnPant(0, BtnPants[0]);
                break;
            case ShopState.ShieldShop:
                TryOnShield(0, BtnShields[0]);
                break;
            default:
                break;
        }
    }

    private void ClearTryOnItem()
    {
        if(player.currentHat != null)
        {
            player.currentHat.gameObject.SetActive(true);           
        }
        if(HatSelected != null)
        {
            Destroy(HatSelected.gameObject);
        }
        if(player.currentShield != null)
        {
            player.currentShield.gameObject.SetActive(true);
        }
        if(ShieldSelected != null)
        {
            Destroy(ShieldSelected.gameObject);
        }
        
        player.pantRen.material = player.currentPant;
    }

    private void LoadShop()
    {
        IsLoaded = true;
        LoadHatItem();
        LoadPantItem();
        LoadShieldItem();
    }

    private Transform GetContentByState(ShopState state)
    {
        for(int i = 0; i<shopTypes.Count;i++)
        {
            if(state == shopTypes[i].shopState)
            {
                return shopTypes[i].ShopContent;
            }
        }
        return null;
    }

    public void TryOnItem(int indexItem, ButtonAction buttonAction)
    {
        if(indexSelected == indexItem)
        {
            return;
        }
        if(ChooseButtonItem != null)
        {
            ChooseButtonItem.DeSelect();
        }
        ChooseButtonItem = buttonAction;
        ChooseButtonItem.Select();
    }
    public void SetDescription(string description)
    {
        Description.text = description;
    }
    private void LoadHatItem()
    {
        int Count = DataManager.Instance.hatData.hatDatas.Length;
        Transform content = GetContentByState(ShopState.HatShop);
        for(int i = 0;i < Count;i++)
        {
            
            ButtonAction NewItem = Instantiate(buttonAction, content).GetComponent<ButtonAction>();
            NewItem.GetComponent<Image>().sprite = DataManager.Instance.hatData.hatDatas[i].Image;
            NewItem.index = i;
            if (SaveManager.Instance.HatStates.ItemStates[i] != 2)
            {
                NewItem.UnLocked();
            }
            NewItem.GetComponent<ButtonAction>().Action += TryOnHat;
            BtnHats.Add(NewItem);
        }
    }

    public void TryOnHat(int indexHat, ButtonAction buttonAction)
    {
        TryOnItem(indexHat, buttonAction);
        if (player.currentHat != null)
        {
            player.currentHat.SetActive(false);
        }
        indexSelected = indexHat;
        HatData hatData = DataManager.Instance.hatData;
        SetStateItem(SaveManager.Instance.HatStates.ItemStates[indexSelected], hatData.Price);
        if (HatSelected != null)
        {
            Destroy(HatSelected.gameObject);
        }
        HatSelected = Instantiate(hatData.hatDatas[indexSelected].HatPrefabs, player.hatPoint);
        SetDescription(hatData.ValueBonus.ToString() + "% Range");
    }
    private void LoadPantItem()
    {
        int Count = DataManager.Instance.pantData.PantDatas.Length;
        Transform content = GetContentByState(ShopState.PantShop);
        for (int i = 0; i < Count; i++)
        {
            ButtonAction NewItem = Instantiate(buttonAction, content).GetComponent<ButtonAction>();
            NewItem.GetComponent<Image>().sprite = DataManager.Instance.pantData.PantDatas[i].Image;
            NewItem.index = i;
            if (SaveManager.Instance.PantStates.ItemStates[i] != 2)
            {
                NewItem.UnLocked();
            }
            NewItem.Action += TryOnPant;
            BtnPants.Add(NewItem);
        }
    }

    public void TryOnPant(int indexPant, ButtonAction buttonAction)
    {
        TryOnItem(indexPant, buttonAction);
        indexSelected = indexPant;
        PantData pantData = DataManager.Instance.pantData;
        SetStateItem(SaveManager.Instance.PantStates.ItemStates[indexSelected], pantData.Price);
        PantSelected = pantData.PantDatas[indexPant].PantMaterial;
        player.pantRen.material = PantSelected;
        SetDescription(pantData.ValueBonus.ToString() + "% Move Speed");
    }

    public void LoadShieldItem()
    {
        int Count = DataManager.Instance.shieldData.ShieldDatas.Length;
        Transform content = GetContentByState(ShopState.ShieldShop);
        for (int i = 0; i < Count; i++)
        {
            ButtonAction NewItem = Instantiate(buttonAction, content).GetComponent<ButtonAction>();
            NewItem.GetComponent<Image>().sprite = DataManager.Instance.shieldData.ShieldDatas[i].Image;
            NewItem.index = i;
            if (SaveManager.Instance.ShieldStates.ItemStates[i] != 2)
            {
                NewItem.UnLocked();
            }
            NewItem.Action += TryOnShield;
            BtnShields.Add(NewItem);
        }
    }
    public void TryOnShield(int indexShield, ButtonAction buttonAction)
    {
        TryOnItem(indexShield, buttonAction);
        if (player.currentShield != null)
        {
            player.currentShield.SetActive(false);
        }
        indexSelected = indexShield;
        ShieldData shieldData = DataManager.Instance.shieldData;
        SetStateItem(SaveManager.Instance.ShieldStates.ItemStates[indexSelected], shieldData.Price);
        if(ShieldSelected != null)
        {
            Destroy(ShieldSelected.gameObject);
        }
        ShieldSelected = Instantiate(shieldData.ShieldDatas[indexShield].ShieldPrefabs, player.shieldPoint);
        SetDescription(shieldData.ValueBonus.ToString() + "% Gold");     
    }
    public void SelectItem()
    {
        SaveItemBase saveItemBase = SaveManager.Instance.GetSaveItemSkin(currentShop.shopState);
        if(saveItemBase ==null|| indexSelected == saveItemBase.currentItem)
        {
            return;
        }
        ChangItem();
        saveItemBase.SaveItem(indexSelected);
        State.text = Constant.STRING_EQUIPED;
    }

    public void BuyItem()
    {
        SaveItemBase saveItemBase = SaveManager.Instance.GetSaveItemSkin(currentShop.shopState);
        SkinData skinData = DataManager.Instance.skinDataDic[currentShop.shopState];
        if (saveItemBase == null || SaveManager.Instance.Coin < skinData.Price)
        {
            return;
        }
        UIManager.Instance.GetUI<CoinUI>().SetCoin(SaveManager.Instance.Coin -= skinData.Price);
        ChangItem();
        saveItemBase.SaveItem(indexSelected);
        OnExit();
    }
    public void ChangItem()
    {
        switch (currentShop.shopState)
        {
            case ShopState.HatShop:
                if(player.currentHat != null)
                {
                    Destroy(player.currentHat.gameObject);
                }
                player.currentHat = Instantiate(HatSelected, player.hatPoint);
                player.currentHat.SetActive(false);
                BtnHats[indexSelected].UnLocked();
                break;
            case ShopState.PantShop:
                player.currentPant = PantSelected;
                BtnPants[indexSelected].UnLocked();
                break;
            case ShopState.ShieldShop:
                if (player.currentShield != null)
                {
                    Destroy(player.currentShield.gameObject);
                }
                player.currentShield = Instantiate(ShieldSelected, player.shieldPoint);
                player.currentShield.SetActive(false);
                BtnShields[indexSelected].UnLocked();
                break;
            default:
                break;
        }
    }
    
}
