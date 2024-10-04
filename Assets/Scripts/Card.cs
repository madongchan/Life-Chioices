using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public TMP_Text dialogueText; // TextMeshProUGUI 컴포넌트를 참조
    public string cardText = ""; // 다음 출력할 텍스트

    // 카드에 대사를 설정하는 메서드
    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue; // 대사 설정
        cardText = dialogue;
    }
}