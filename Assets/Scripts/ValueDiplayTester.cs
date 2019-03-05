using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueDiplayTester : MonoBehaviour {

    public Color StartColor;
    public bool EnableEffect = false;
    ValueDisplayController controller;

	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<ValueDisplayController>();
        controller.EnableEffectOnChange = EnableEffect;
        controller.SetValue("10");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
