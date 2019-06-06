using System;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisplayController : MonoBehaviour {

    public Text Lable;

    public Action<string> OnValueChanged;

    #region API

    public void SetValue(string _value) {
        if(Lable)
            Lable.text = _value;
        if (OnValueChanged != null)
            OnValueChanged(_value);
    }

    #endregion
}