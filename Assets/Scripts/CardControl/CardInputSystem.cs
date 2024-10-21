using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInputSystem : MonoBehaviour {
    enum ENUM_CARD_INPUT_STATE {
        IN_HAND,      // 손패에 있는 상태
        IN_HAND_HOLD  // 손에 들고 있는 상태
    }

    public Material dissolveMaterial; // 카드가 사라지는 효과를 위한 Material
    private Vector3 originalMousePos;
    private ENUM_CARD_INPUT_STATE state;
    [SerializeField] public SpriteRenderer card = null;

    private void Start() {
        SetCardState(ENUM_CARD_INPUT_STATE.IN_HAND);
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
            //Debug.Log("카드 위에 마우스가 들어옴.");
        }
    }

    private void OnMouseExit() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (state == ENUM_CARD_INPUT_STATE.IN_HAND) {
            //Debug.Log("카드 위에서 마우스가 나감.");
        }
    }

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        originalMousePos = transform.position;
        //Debug.Log("카드 클릭됨.");
        SetCardState(ENUM_CARD_INPUT_STATE.IN_HAND_HOLD);
    }

    private void OnMouseDrag() {
        if (state == ENUM_CARD_INPUT_STATE.IN_HAND_HOLD) {
            // 카드 위치 업데이트
            transform.position = GetMousePosition();

            if (IsCardInUsageColliderLayer()) {
                // 밝은 노란색으로 변경 (카드 사용 가능한 상태)
                card.color = new Color(card.color.r, card.color.g, card.color.b, 0.2f);
            }
            else {
                card.color = new Color(card.color.r, card.color.g, card.color.b, 1f);
            }
            // 드래그가 감지되면 상태를 유지
            //Debug.Log("카드가 드래그됨.");
        }
    }

    private void OnMouseUp() {
        if (state == ENUM_CARD_INPUT_STATE.IN_HAND_HOLD) {
            //Debug.Log("카드를 놓음.");
            if (IsCardInUsageColliderLayer()) {
                // 0에서 1로 페이드 아웃 (디졸브)
                dissolveMaterial.DOFloat(1f, "_AlphaTransitionProgress", 1).OnComplete(() => {
                    // 1에서 0으로 다시 복구 (다시 나타남)
                    dissolveMaterial.DOFloat(0f, "_AlphaTransitionProgress", 0);
                    //var stateManager = GameManager.GetInstance().characterStateManager;
                    //stateManager.SetSliderValue(0, stateManager.GetSliderValue(0) + 0.1f);
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
