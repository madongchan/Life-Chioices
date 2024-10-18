using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateSliderManager : MonoBehaviour {
    // 슬라이더 배열
    public Slider[] sliders;
    public Color32 defaultColor = new Color32(255, 158, 61, 255); // 기본 색상 (주황색)

    public Color highlightColor = Color.green; // 값 변경 시 강조할 색상
    public float highlightDuration = 1f; // 강조 색상이 유지될 시간 (초)

    public void Initialize() {
        foreach (var slider in sliders) {
            slider.value = 0.5f; // 기본 값 설정
            slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(slider); });
        }
    }

    private void OnSliderValueChanged(Slider changedSlider) {
        // 슬라이더 값이 변경되었을 때의 로직
        //Debug.Log($"슬라이더 {changedSlider.name}의 값: {changedSlider.value}");
        // 추가적인 슬라이더 값 처리 로직 작성 가능
    }

    // 슬라이더 값 설정 및 색상 강조
    public void SetSliderValue(int index, float value) {
        if (index >= 0 && index < sliders.Length) {
            sliders[index].value = value; // 지정된 인덱스의 슬라이더 값 설정
            StartCoroutine(HighlightSlider(sliders[index])); // 슬라이더 색상 강조
        }
    }

    // 슬라이더를 일시적으로 강조하는 코루틴
    private IEnumerator HighlightSlider(Slider slider) {
        // 슬라이더의 Fill Area의 색상 변경
        Image fillImage = slider.fillRect.GetComponent<Image>();
        if (fillImage != null) {
            fillImage.color = highlightColor; // 색상을 초록색으로 변경
            yield return new WaitForSeconds(highlightDuration); // 지정된 시간만큼 대기
            fillImage.color = defaultColor; // 다시 원래 색상으로 복귀
        }
    }

    public float GetSliderValue(int index) {
        if (index >= 0 && index < sliders.Length) {
            return sliders[index].value; // 지정된 인덱스의 슬라이더 값 반환
        }
        return 0f; // 잘못된 인덱스 경우 기본값 반환
    }
}
