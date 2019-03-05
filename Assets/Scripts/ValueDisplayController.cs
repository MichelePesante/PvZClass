using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisplayController : MonoBehaviour {

    public Text Lable;
    public Image BGColorImage;
    public Image ImageEffect;
    public Color StartColor;

    public bool EnableEffectOnChange;

    public Action<string> OnValueChanged;

    private void Awake() {
        //SetColor(StartColor);
    }

    #region API

    public void SetValue(string _value) {
        Lable.text = _value;
        EnableEffect(EnableEffectOnChange);
        if (OnValueChanged != null)
            OnValueChanged(_value);
    }

    //public void SetColor(Color _color) {
    //    if(BGColorImage)
    //        BGColorImage.color = _color;
    //}

    public void EnableEffect(bool enableEffect) {
        ImageEffect.enabled = enableEffect;
    }

    #endregion

}
