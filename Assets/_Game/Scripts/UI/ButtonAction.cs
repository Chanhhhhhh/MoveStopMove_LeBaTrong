using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonActiom : MonoBehaviour
{
    UnityAction Action;
    RectTransform rectTransform;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        Action.Invoke();
    }
}
