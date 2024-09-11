using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [SerializeField] private GameObject buttonAction;
    [SerializeField] private List<ShopType> shopTypes;
    private ShopType currentShop;
    private ShopState currentShopState;

    private GameObject HatSelected;
    private Material PantSelected;
    private GameObject ShieldSelected;

    public int indexSelected;

    private void Start()
    {
        LoadShop(); 
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
        OpenShopView(shopTypes[0]);
        
    }

    public override void OnExit()
    {
        ClearTryOnItem();
        base.OnExit();

    }
    private void OnClickShopBtn(ShopType shopType)
    {
        BtnAction?.Invoke(shopType);
    }
    private void OpenShopView(ShopType shopType)
    {
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
                TryOnHat(0);
                break;
            case ShopState.PantShop:
                TryOnPant(0);
                break;
            case ShopState.ShieldShop:
                TryOnShield(0);
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
        player.pantRen.material = player.currentPant;
    }

    private void LoadShop()
    {
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

    private void LoadHatItem()
    {
        int Count = DataManager.Instance.hatData.hatDatas.Length;
        Transform content = GetContentByState(ShopState.HatShop);
        for(int i = 0;i < Count;i++)
        {
            GameObject NewItem = Instantiate(buttonAction, content);
            NewItem.GetComponent<Image>().sprite = DataManager.Instance.hatData.hatDatas[i].Image;
            NewItem.GetComponent<ButtonAction>().index = i;
            NewItem.GetComponent<ButtonAction>().Action += TryOnHat;
        }
    }

    public void TryOnHat(int indexHat)
    {
        if(player.currentHat != null)
        {
            player.currentHat.SetActive(false);
        }
        if(indexHat == indexSelected)
        {
            return;
        }
        indexSelected = indexHat;
        if(HatSelected != null)
        {
            Destroy(HatSelected.gameObject);
        }
        HatSelected = Instantiate(DataManager.Instance.hatData.hatDatas[indexHat].HatPrefabs, player.hatPoint);
    }
    private void LoadPantItem()
    {
        int Count = DataManager.Instance.pantData.PantDatas.Length;
        Transform content = GetContentByState(ShopState.PantShop);
        for (int i = 0; i < Count; i++)
        {
            GameObject NewItem = Instantiate(buttonAction, content);
            NewItem.GetComponent<Image>().sprite = DataManager.Instance.pantData.PantDatas[i].Image;
            NewItem.GetComponent<ButtonAction>().index = i;
            NewItem.GetComponent<ButtonAction>().Action += TryOnPant;
        }
    }

    public void TryOnPant(int indexPant)
    {
        if (indexPant == indexSelected)
        {
            return;
        }
        indexSelected = indexPant;
        PantSelected = DataManager.Instance.pantData.PantDatas[indexPant].PantMaterial;
        player.pantRen.material = PantSelected;
    }

    public void LoadShieldItem()
    {
        int Count = DataManager.Instance.shieldData.ShieldDatas.Length;
        Transform content = GetContentByState(ShopState.ShieldShop);
        for (int i = 0; i < Count; i++)
        {
            GameObject NewItem = Instantiate(buttonAction, content);
            NewItem.GetComponent<Image>().sprite = DataManager.Instance.shieldData.ShieldDatas[i].Image;
            NewItem.GetComponent<ButtonAction>().index = i;
            NewItem.GetComponent<ButtonAction>().Action += TryOnShield;
        }
    }
    public void TryOnShield(int indexShield)
    {
        if(player.currentShield != null)
        {
            player.currentShield.SetActive(false);
        }
        if(indexShield == indexSelected)
        {
            return;
        }
        indexSelected = indexShield;
        if(ShieldSelected != null)
        {
            Destroy(ShieldSelected.gameObject);
        }
        ShieldSelected = Instantiate(DataManager.Instance.shieldData.ShieldDatas[indexShield].ShieldPrefabs, player.shieldPoint);
         
        
    }
    public void SelectItem()
    {

    }

    public void BuyItem()
    {

    }
    public void ChangItem()
    {

    }
}
