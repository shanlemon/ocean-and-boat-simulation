using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{

    private Slider wheelSlider;

    private void Start() {
        wheelSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float val = WheelRotator.Instance.angle / WheelRotator.Instance.maxTurnAngle;
        wheelSlider.value = (val + 1) / 2;
    }
}
