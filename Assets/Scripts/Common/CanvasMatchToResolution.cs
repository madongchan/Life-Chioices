using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
[ExecuteInEditMode]
public class CanvasMatchToResolution : MonoBehaviour
{
    CanvasScaler scaler;
    float xScreenResolution;
    float yScreenResolution;
    float currentScreenRatio; // // 1.77에 가까울수록 match -> 0으로 2.45에 가까울수록 match -> 1로
    float ratioDivider;

    private void Awake() {
        MatchResoluiton();
    }

    public void MatchResoluiton() {
        yScreenResolution = Camera.main.orthographicSize;
        xScreenResolution = yScreenResolution * Camera.main.aspect;
        currentScreenRatio = xScreenResolution / yScreenResolution;
        ratioDivider = 0.68f;
        scaler = gameObject.GetComponent<CanvasScaler>();
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = (currentScreenRatio-1.7f)/ratioDivider;

        if(xScreenResolution/yScreenResolution > 2) { scaler.matchWidthOrHeight = 1; }
    }
}
