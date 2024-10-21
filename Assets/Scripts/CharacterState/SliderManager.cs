using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]  // 에디터 환경에서도 실행되도록 설정
public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider[] sliders;  // 에디터에서 할당할 Slider 배열
    [SerializeField] private float spacing = 100f;  // 슬라이더 간의 X축 간격

    private void OnValidate()
    {
        // 에디터에서 슬라이더 간격을 실시간으로 조정 가능
        UpdateSliderPositions();
    }

    // 간격에 맞춰 슬라이더 위치를 업데이트하는 메서드
    private void UpdateSliderPositions()
    {
        if (sliders == null || sliders.Length == 0)
            return;

        // 첫 번째 슬라이더의 기준 위치 설정
        Vector3 startPos = sliders[0].transform.localPosition;

        // 배열에 있는 슬라이더들의 위치를 순차적으로 설정
        for (int i = 1; i < sliders.Length; i++)
        {
            // X축 간격 적용 (Y축과 Z축은 동일하게 유지)
            Vector3 newPos = startPos + new Vector3(spacing * i, 0, 0);  // X축 기준으로 spacing만큼 오른쪽으로 배치
            sliders[i].transform.localPosition = newPos;
        }
    }
}
