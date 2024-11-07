using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateSliderManager : MonoBehaviour {
    // 슬라이더 배열
    public Slider[] sliders;
    public Color32 defaultColor = new Color32(255, 158, 61, 255); // 기본 색상 (주황색)

    public Color increaseColor = Color.green; // 값 변경 증가 시 강조할 색상
    public Color decreaseColor = Color.red; // 값 변경 감소 시 강조할 색상
    public float highlightDuration = 1f; // 강조 색상이 유지될 시간 (초)

    private bool isMouseOverforStoryBox = false;

    public void Initialize() {
        CharacterStatus characterStatus = CharacterStatusDAO.GetCharacterStatus();
        foreach (Enum_CharcterState state in Enum.GetValues(typeof(Enum_CharcterState))) {
            int index = (int)state;
            float value = 0f;
            switch (state) {
                case Enum_CharcterState.HP:
                    value = characterStatus.HP / 100f;
                    break;
                case Enum_CharcterState.Intelligence:
                    value = characterStatus.Intelligence / 100f;
                    break;
                case Enum_CharcterState.Happiness:
                    value = characterStatus.Happiness / 100f;
                    break;
                case Enum_CharcterState.Charm:
                    value = characterStatus.Charm / 100f;
                    break;
                case Enum_CharcterState.Money:
                    value = characterStatus.Money / 100f;
                    break;
            }
            sliders[index].value = value; // 지정된 인덱스의 슬라이더 값 설정
            sliders[index].onValueChanged.AddListener(delegate { OnSliderValueChanged(sliders[index]); });
        }
    }

    private void OnSliderValueChanged(Slider changedSlider) {
        // 슬라이더 값이 변경되었을 때의 로직
        //Debug.Log($"슬라이더 {changedSlider.name}의 값: {changedSlider.value}");
        // 추가적인 슬라이더 값 처리 로직 작성 가능
    }

    // 슬라이더 값 설정 및 색상 강조
    public void PreSetSliderValue(int HP, int Intelligence, int Happiness, int Charm, int Money, bool isMouseOver = false) {
        int isPositive = 0;
        if (isMouseOver) {
            isMouseOverforStoryBox = true;
            int index = 0;
            foreach (Enum_CharcterState state in Enum.GetValues(typeof(Enum_CharcterState))) {
                index = (int)state;
                float previousValue = sliders[index].value;
                float value = 0f;
                switch (state) {
                    case Enum_CharcterState.HP:
                        value = previousValue + (HP / 100f);
                        break;
                    case Enum_CharcterState.Intelligence:
                        value = previousValue + (Intelligence / 100f);
                        break;
                    case Enum_CharcterState.Happiness:
                        value = previousValue + (Happiness / 100f);
                        break;
                    case Enum_CharcterState.Charm:
                        value = previousValue + (Charm / 100f);
                        break;
                    case Enum_CharcterState.Money:
                        value = previousValue + (Money / 100f);
                        break;
                }
                sliders[index].value = value; // 지정된 인덱스의 슬라이더 값 설정
                Color highlightColor;
                if (value > previousValue) {
                    isPositive++;
                    highlightColor = increaseColor;
                }
                else if (value < previousValue) {
                    highlightColor = decreaseColor;
                }
                else {
                    highlightColor = defaultColor;
                }
                HighlightSlider(sliders[index], highlightColor); // 슬라이더 색상 강조
            }
            if (isPositive > 2) {
                GameManager.GetInstance().particlesManager.PlayParticle(true);
            }
            else {
                GameManager.GetInstance().particlesManager.PlayParticle(false);
            }
        }
        else if (isMouseOverforStoryBox) {
            isMouseOverforStoryBox = false;
            // 변경했던 값과 highlightColor를 초기화
            int index = 0;
            foreach (Enum_CharcterState state in Enum.GetValues(typeof(Enum_CharcterState))) {
                index = (int)state;
                float previousValue = sliders[index].value;
                float value = 0f;
                switch (state) {
                    case Enum_CharcterState.HP:
                        value = previousValue - HP / 100f;
                        break;
                    case Enum_CharcterState.Intelligence:
                        value = previousValue - Intelligence / 100f;
                        break;
                    case Enum_CharcterState.Happiness:
                        value = previousValue - Happiness / 100f;
                        break;
                    case Enum_CharcterState.Charm:
                        value = previousValue - Charm / 100f;
                        break;
                    case Enum_CharcterState.Money:
                        value = previousValue - Money / 100f;
                        break;
                }
                sliders[index].value = value; // 지정된 인덱스의 슬라이더 값 설정
                HighlightSlider(sliders[index], defaultColor); // 슬라이더 색상 강조
            }
        }

    }

    // 슬라이더 하이라이트 색상을 기본 색상으로 변경
    public void ResetSliderColor() {
        foreach (Slider slider in sliders) {
            Image fillImage = slider.fillRect.GetComponent<Image>();
            if (fillImage != null) {
                fillImage.DOColor(defaultColor, 0); // 색상을 기본 색상으로 변경
            }
        }
    }

    // 슬라이더를 일시적으로 강조하는 코루틴
    public void HighlightSlider(Slider slider, Color highlightColor) {
        // 슬라이더의 Fill Area의 색상 변경
        Image fillImage = slider.fillRect.GetComponent<Image>();
        if (fillImage != null) {
            fillImage.DOColor(highlightColor, 0.5f); // 색상을 highlightColor로 변경
        }
    }

    public float GetSliderValue(int index) {
        if (index >= 0 && index < sliders.Length) {
            return sliders[index].value; // 지정된 인덱스의 슬라이더 값 반환
        }
        return 0f; // 잘못된 인덱스 경우 기본값 반환
    }
}
