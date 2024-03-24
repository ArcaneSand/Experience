using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HoverManager : MonoBehaviour
{
    public TextMeshProUGUI tipText; 
    public RectTransform tipWindow;
    public static Action<string,Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable() {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }

    private void OnDisable() {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    private void Start() {
        HideTip();
    }

    private void ShowTip(string tip,Vector2 mousePos){
        tipText.text = tip;
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x*0.6f, mousePos.y);
    }

    private void HideTip(){
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
