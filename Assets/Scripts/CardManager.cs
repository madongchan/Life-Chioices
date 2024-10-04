using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardSpawnPoint;
    public float cardWidth = 1f; // 카드의 너비
    public float minSpacing = 0.1f; // 카드 사이의 최소 간격
    public float maxSpacing = 1.5f; // 카드 사이의 최대 간격
    public float maxTotalWidth = 8f; // 카드들의 최대 전체 너비
    private List<GameObject> currentCards = new List<GameObject>();

    // 카드 개수와 텍스트를 업데이트하는 메서드
    public void ShowCardSelection(int cardCount, params string[] cardTexts)
    {
        ClearCards(); // 기존 카드 제거
        if (cardTexts.Length != cardCount)
        {
            Debug.LogError("카드 개수와 텍스트 배열의 크기가 일치하지 않습니다.");
            return;
        }

        float totalWidth = CalculateTotalWidth(cardCount);
        float spacing = CalculateSpacing(cardCount, totalWidth);
        Vector3 startPosition = CalculateStartPosition(cardCount, totalWidth, spacing);

        for (int i = 0; i < cardCount; i++)
        {
            Vector3 cardPosition = startPosition + new Vector3(i * (cardWidth + spacing), 0, 0);
            GameObject card = Instantiate(cardPrefab, cardSpawnPoint.position + cardPosition, Quaternion.identity);
            card.GetComponent<Card>().SetDialogue(cardTexts[i]);
            currentCards.Add(card); // 현재 카드 목록에 추가
        }
    }

    // 카드 제거 메서드
    private void ClearCards()
    {
        foreach (GameObject card in currentCards)
        {
            Destroy(card);
        }
        currentCards.Clear();
    }

    private float CalculateTotalWidth(int cardCount)
    {
        float totalWidth = cardCount * cardWidth + (cardCount - 1) * maxSpacing;
        return Mathf.Min(totalWidth, maxTotalWidth);
    }

    private float CalculateSpacing(int cardCount, float totalWidth)
    {
        if (cardCount <= 1) return 0;
        float availableSpace = totalWidth - (cardCount * cardWidth);
        float calculatedSpacing = availableSpace / (cardCount - 1);
        return Mathf.Clamp(calculatedSpacing, minSpacing, maxSpacing);
    }

    private Vector3 CalculateStartPosition(int cardCount, float totalWidth, float spacing)
    {
        float totalSpacing = (cardCount - 1) * spacing;

        // 짝수 카드일 경우 중앙의 두 카드 사이의 간격을 반영한 시작 X 위치
        float startX = -((totalWidth - cardWidth) / 2);

        return new Vector3(startX, 0, 0);
    }

}
