using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInputSystem : MonoBehaviour {
    enum ENUM_CARD_INPUT_STATE {
        IN_HAND,      // 손패에 있는 상태
        IN_HAND_HOLD  // 손에 들고 있는 상태
    }
    public Card cardData; // 카드 데이터
    public Material dissolveMaterial; // 카드가 사라지는 효과를 위한 Material
    public GameObject cardIMG = null;
    public TextMeshPro cardDescription = null;
    // 앞면 스프라이트 이미지
    public Sprite frontSprite;

    private Vector3 originalMousePos;
    private ENUM_CARD_INPUT_STATE state;

    private void Start() {
        SetCardState(ENUM_CARD_INPUT_STATE.IN_HAND);
    }

    public void ShowFrontImage() {
        cardIMG.GetComponent<SpriteRenderer>().sprite = frontSprite;
        // cardIMG의 레이어 순서를 1로 설정
        cardIMG.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    private void SetCardState(ENUM_CARD_INPUT_STATE newState) {
        state = newState;
    }
    [Tooltip("드래그 저항 값 (값이 높을수록 카드가 쉽게 드래그되지 않음)")]
    [SerializeField] private float dragResistance = 0.2f;
    private Vector3 GetMousePosition() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // 2D 환경을 위한 Z축 좌표 설정
        return mousePosition;
    }

    private bool IsCardInUsageColliderLayer() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 100.0f;
        int layerMask = 1 << LayerMask.NameToLayer("CardUsageCollider");
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, distance, layerMask)) {
            return true;
        }
        return false;
    }

    private bool IsCardDragged() {
        Vector3 mouseMovement = GetMousePosition() - originalMousePos;
        return mouseMovement.magnitude > dragResistance;
    }

    private void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (state == ENUM_CARD_INPUT_STATE.IN_HAND) {
            // cardIMG를 DoTween을 이용해 1.5배 확대
            cardIMG.transform.DOScale(1.1f, 0.1f).SetEase(Ease.OutBack);
            //Debug.Log("카드 위에 마우스가 들어옴.");
        }
    }

    private void OnMouseExit() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (state == ENUM_CARD_INPUT_STATE.IN_HAND) {
            cardIMG.transform.DOScale(1f, 0.1f).SetEase(Ease.InBack);
            //Debug.Log("카드 위에서 마우스가 나감.");
        }
    }

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        originalMousePos = transform.position;
        //Debug.Log("카드 클릭됨.");
        cardIMG.transform.DOScale(1f, 0);
        SetCardState(ENUM_CARD_INPUT_STATE.IN_HAND_HOLD);
    }

    private bool isCardInUsage = false; // 카드 사용 상태를 추적하는 변수

    private void OnMouseDrag() {
        if (state == ENUM_CARD_INPUT_STATE.IN_HAND_HOLD) {
            // 카드 위치 업데이트
            transform.position = GetMousePosition();

            bool currentCardInUsage = IsCardInUsageColliderLayer(); // 현재 카드가 사용 가능 영역에 있는지 확인

            if (currentCardInUsage && !isCardInUsage) {
                // 카드가 사용 가능한 상태로 들어갔을 때 한 번 실행
                isCardInUsage = true;
                cardIMG.SetActive(false);
                GameManager.GetInstance().characterStateSlideManager.PreSetSliderValue(cardData.HP, cardData.Intelligence, cardData.Happiness, cardData.Charm, cardData.Money, true);
            }
            else if (!currentCardInUsage && isCardInUsage) {
                // 카드가 사용 가능한 상태에서 벗어났을 때 한 번 실행
                isCardInUsage = false;
                cardIMG.SetActive(true);
                GameManager.GetInstance().characterStateSlideManager.PreSetSliderValue(cardData.HP, cardData.Intelligence, cardData.Happiness, cardData.Charm, cardData.Money, false);
                GameManager.GetInstance().particlesManager.StopParticle();
            }

            // 드래그가 감지되면 상태를 유지
            // Debug.Log("카드가 드래그됨.");
        }
    }


    private void OnMouseUp() {
        if (state == ENUM_CARD_INPUT_STATE.IN_HAND_HOLD) {
            //Debug.Log("카드를 놓음.");
            if (IsCardInUsageColliderLayer()) {
                // 0에서 1로 페이드 아웃 (디졸브)
                // cardDescription 비활성화
                cardDescription.gameObject.SetActive(false);
                dissolveMaterial.DOFloat(0.6f, "_AlphaTransitionProgress", 1.2f).OnComplete(() => {
                    // 1에서 0으로 다시 복구 (다시 나타남)
                    dissolveMaterial.DOFloat(0f, "_AlphaTransitionProgress", 0);
                    CharacterStatusDAO.UpdateCharacterStatus(cardData.HP, cardData.Intelligence, cardData.Happiness, cardData.Charm, cardData.Money);
                    GameManager.GetInstance().particlesManager.StopParticle();
                    GameManager.GetInstance().characterStateSlideManager.ResetSliderColor();
                    var action = GameManager.GetInstance().cardManager.ShowCardSelection(cardData.NextIndex);
                    action();
                });
            }
            else {
                transform.position = originalMousePos;
                Debug.Log("카드가 사용되지 않음.");
                // 카드가 사용되지 않은 경우 손패로 돌아가는 로직 추가
                SetCardState(ENUM_CARD_INPUT_STATE.IN_HAND);
            }
        }
    }
}
