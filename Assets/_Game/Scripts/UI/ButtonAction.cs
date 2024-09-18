using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private GameObject Frame_ChooseItem;
    [SerializeField] private GameObject Icon_Block;
    public UnityAction<int, ButtonAction> Action;
    public int index;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        SoundManager.Instance.PlaySoundClip(SoundType.BtnClick);
        Action.Invoke(index, this);
    }

    public void Select()
    {
        Frame_ChooseItem.SetActive(true);
    }
    public void DeSelect()
    {
        Frame_ChooseItem.SetActive(false);
    }
    public void UnLocked()
    {
        Icon_Block.SetActive(false);
    }

    public void Locked()
    {
        Icon_Block.SetActive(true);
    }
}
