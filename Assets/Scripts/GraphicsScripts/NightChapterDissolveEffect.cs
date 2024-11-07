using System.Collections;
using UnityEngine;
using DG.Tweening;
using Febucci.UI;
using System.Linq;
using System;

public class NightChapterDissolveEffect : MonoBehaviour {
    public GameObject storyChapterBox;        // 머티리얼을 적용할 게임 오브젝트
    public GameObject NightBG;           // 밤 배경이 있는 게임 오브젝트
    public GameObject StorychapterBox;           // 스토리 챕터 박스 게임 오브젝트
    public GameObject[] textObjects;          // 텍스트 게임 오브젝트 배열
    public float dissolveDuration = 2f;       // 디졸브 애니메이션 시간
    public float fadeDuration = 2f;           // 페이드 애니메이션 시간

    private Material dissolveMaterial;        // 디졸브에 사용할 머티리얼
    private SpriteRenderer spriteRenderer;    // 스프라이트 렌더러

    void Start() {
        // StoryChapterBox의 머티리얼 가져오기
        if (storyChapterBox != null) {
            SpriteRenderer renderer = storyChapterBox.GetComponent<SpriteRenderer>();
            dissolveMaterial = renderer.material;
            spriteRenderer = NightBG.GetComponent<SpriteRenderer>();
        }
    }

    public void StartDissolveEffect(Action action = null) {
        // 밤 배경 초기화
        spriteRenderer.DOFade(1f, 0).OnComplete(() => {
            NightBG.SetActive(true);
            StorychapterBox.SetActive(true);
            ActivateTextObjects(true);
        });
        // DOTween을 사용한 디졸브 효과
        DOTween.To(() => dissolveMaterial.GetFloat("_AlphaTransitionProgress"),
            x => dissolveMaterial.SetFloat("_AlphaTransitionProgress", x),
            1f, dissolveDuration).SetEase(Ease.InOutSine).OnComplete(() => {
                // 페이드 아웃 후 텍스트 오브젝트 비활성화
                ActivateTextObjects(false);
                //_AlphaTransitionProgress 값을 0으로 설정하여 디졸브 효과 제거하면 스토리 챕터 박스가 보이게 됨 그래서 스토리 챕터 박스를 비활성화
                StorychapterBox.SetActive(false);
                dissolveMaterial.SetFloat("_AlphaTransitionProgress", 0f);
                if (spriteRenderer != null) {
                    // DOTween을 사용한 페이드 아웃 효과
                    spriteRenderer.DOFade(0f, fadeDuration).OnComplete(() => {
                        NightBG.SetActive(false);
                        action?.Invoke(); // Action 실행
                    });
                }
            });
    }

    // 텍스트 오브젝트 배열을 매개변수로 받아 활성화 여부를 결정하는 함수
    void ActivateTextObjects(bool activate) {
        foreach (GameObject textObject in textObjects) {
            if (textObject != null) {
                textObject.SetActive(activate);  // 텍스트 오브젝트의 활성화 여부 설정
            }
        }
    }

    internal void StartDissolveEffect(object v) {
        throw new NotImplementedException();
    }
}
