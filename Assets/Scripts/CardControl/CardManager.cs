using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public GameObject cardPrefab;
    public Transform cardSpawnPoint;
    public float cardWidth = 1f; // 카드의 너비
    public float minSpacing = 0.1f; // 카드 사이의 최소 간격
    public float maxSpacing = 1.5f; // 카드 사이의 최대 간격
    public float maxTotalWidth = 8f; // 카드들의 최대 전체 너비
    private List<GameObject> currentCards = new List<GameObject>();

    // 카드 개수와 텍스트를 업데이트하는 메서드
    public Action ShowCardSelection(int NextStoryIndex) {
        return () => {
            ClearCards(); // 기존 카드 제거
            int cardCount = StoryDAO.GetStoryCardList(NextStoryIndex).Count;
            float totalWidth = CalculateTotalWidth(cardCount);
            float spacing = CalculateSpacing(cardCount, totalWidth);
            Vector3 startPosition = CalculateStartPosition(cardCount, totalWidth, spacing);

            for (int i = 0; i < cardCount; i++) {
                Vector3 cardPosition = startPosition + new Vector3(i * (cardWidth + spacing), 0, 0);
                GameObject card = Instantiate(cardPrefab, startPosition, Quaternion.identity);
                // 현재 카드 목록에 추가
                currentCards.Add(card);
                SpreadCards();
                // 카드 데이터 설정
                Card cardData = CardDAO.GetCard(StoryDAO.GetStoryCardList(NextStoryIndex)[i]);
                CardInputSystem cardInputSystem = card.GetComponent<CardInputSystem>();
                // 카드 데이터 설정
                cardInputSystem.cardData = cardData;
                // 카드 텍스트 설정
                cardInputSystem.cardDescription.text = cardData.Description;
                GameManager.GetInstance().textManager.ShowText(NextStoryIndex);
            }
        };
    }

    // 카드 제거 메서드
    private void ClearCards() {
        foreach (GameObject card in currentCards) {
            Destroy(card);
        }
        currentCards.Clear();
    }

    // 펼쳐지는 효과 함수
    public void SpreadCards() {
        Vector3 startPosition = cardSpawnPoint.position; // 카드의 시작 위치를 기준으로 합니다
        int cardCount = currentCards.Count;
        float totalWidth = CalculateTotalWidth(cardCount);
        float spacing = CalculateSpacing(cardCount, totalWidth);

        for (int i = 0; i < cardCount; i++) {
            GameObject card = currentCards[i];
            Vector3 middletargetPosition = startPosition + new Vector3(0, 3, 0);
            Vector3 targetPosition = startPosition + CalculateStartPosition(cardCount, totalWidth, spacing) + new Vector3(i * (cardWidth + spacing), 3, 0);

            // 각 카드는 시작 위치에서 이동하도록 초기화합니다
            card.transform.position = startPosition;

            // 이동과 함께 카드의 펼쳐지는 애니메이션을 설정
            Sequence cardSequence = DOTween.Sequence();

            // 카드의 위치를 목표 위치로 부드럽게 이동
            cardSequence.Append(card.transform.DOMove(middletargetPosition, 0.7f).SetEase(Ease.OutBack));
            // targetPosition으로 이동
            cardSequence.Insert(0.7f, card.transform.DOMove(targetPosition, 0.3f).SetEase(Ease.OutBack));
            // 카드가 중간 위치에 도달하면 회전 효과를 추가하여 카드가 90도에서 앞면 이미지로 변경 후 180도까지 회전하여 앞면이 보이도록 설정
            cardSequence.Insert(0.8f, card.transform.DORotate(new Vector3(0, 90, 0), 0.3f)).OnComplete(() => {
                // 카드 앞면 이미지로 변경
                CardInputSystem cardInputSystem = card.GetComponent<CardInputSystem>();
                cardInputSystem.ShowFrontImage();
                cardSequence.Append(card.transform.DORotate(new Vector3(0, 0, 0), 0.3f)); // 앞면이 보이도록 180도까지 회전하여 뒤집기 완료
            });
        }
    }


    private float CalculateTotalWidth(int cardCount) {
        float totalWidth = cardCount * cardWidth + (cardCount - 1) * maxSpacing;
        //return Mathf.Min(totalWidth, maxTotalWidth);
        return totalWidth;
    }

    private float CalculateSpacing(int cardCount, float totalWidth) {
        if (cardCount <= 1) return 0;
        float availableSpace = totalWidth - (cardCount * cardWidth);
        float calculatedSpacing = availableSpace / (cardCount - 1);
        return Mathf.Clamp(calculatedSpacing, minSpacing, maxSpacing);
    }

    private Vector3 CalculateStartPosition(int cardCount, float totalWidth, float spacing) {
        // 화면 중앙에 카드가 균등하게 배치되도록 시작 위치 계산
        float startX = -(totalWidth - cardWidth) / 2;
        return new Vector3(startX, 0, 0);
    }
}
