using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public UnityAction<int> Action;
    public int index;
    RectTransform rectTransform;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        Action.Invoke(index);
    }
}
