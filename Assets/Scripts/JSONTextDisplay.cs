using System.Collections;
using TMPro;
using UnityEngine;

public class JSONTextDisplay : MonoBehaviour
{
    public TMP_Text tmpText;       // TextMeshPro 컴포넌트
    public float delay = 0.1f;     // 한 글자 출력 간 딜레이 시간
    private string currentText = "";        // 현재 출력된 텍스트
    private TextDataManager textDataManager;  // TextDataManager 인스턴스

    void Start()
    {
        // TextDataManager 인스턴스를 찾아 설정
        textDataManager = FindObjectOfType<TextDataManager>();

        // 카드 선택 후에 텍스트를 출력
        StartCoroutine(DisplayNextText("intro"));
        GameManager.GetInstance().cardManager.ShowCardSelection(2, "남성으로 태어나시겠습니까?", "여성으로 태어나시겠습니까?");
    }

    // 다음 텍스트를 출력하는 코루틴
    public IEnumerator DisplayNextText(string currentTextId = "intro")
    {
        // 현재 출력 중인 텍스트가 있다면 중지
        if (!string.IsNullOrEmpty(currentText))
        {
            StopAllCoroutines();
        }
        // TextDataManager에서 해당 ID로 텍스트를 가져옴
        string textContent;
        if (currentTextId.Contains("기회") || currentTextId.Contains("이제"))
        {
            textContent = currentTextId;
        }
        else
        {
            textContent = textDataManager.GetText(currentTextId);
        }

        if (!string.IsNullOrEmpty(textContent))
        {
            yield return StartCoroutine(TypeText(textContent)); // 한 글자씩 출력
        }
        else
        {
            Debug.LogError($"ID가 '{currentTextId}'인 텍스트를 찾을 수 없습니다.");
        }
    }

    // 한 글자씩 텍스트를 출력하는 코루틴
    IEnumerator TypeText(string fullText)
    {
        currentText = "";
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            tmpText.text = currentText;
            yield return new WaitForSeconds(delay); // 설정된 딜레이만큼 대기
        }

        // 텍스트 출력이 끝난 후 잠시 대기
        yield return new WaitForSeconds(0.2f); // 다음 텍스트 출력 전 잠시 대기
    }
}
