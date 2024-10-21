using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

public class LodingSceneCardClickEvent : MonoBehaviour
{
    [SerializeField] private GameObject fadePanel;  // 이미 생성된 FadePanel을 에디터에서 할당
    [SerializeField] private float hoverScale = 1.2f; // 마우스 오버 시 커질 크기
    [SerializeField] private float hoverDuration = 0.2f; // 커지는 애니메이션의 지속 시간
    [SerializeField] private float fadeDuration = 1.0f; // 페이드 애니메이션의 지속 시간

    private Vector3 originalScale;  // 카드의 원래 크기

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseEnter()
    {

        // 카드 크기를 hoverScale만큼 키우는 애니메이션
        transform.DOScale(originalScale * hoverScale, hoverDuration).SetEase(Ease.OutBack);
    }

    private void OnMouseExit()
    {

        // 카드 크기를 원래 크기로 되돌리는 애니메이션
        transform.DOScale(originalScale, hoverDuration).SetEase(Ease.InBack);
    }

    private void OnMouseDown()
    {

        // 화면이 하얗게 페이드 인 되면서 씬 전환
        StartCoroutine(FadeAndLoadScene("MainScene"));
    }

private IEnumerator FadeAndLoadScene(string sceneName)
{
    if (fadePanel == null)
    {
        Debug.LogError("FadePanel이 할당되지 않았습니다.");
        yield break;
    }

    // 기존의 FadePanel에서 SpriteRenderer 가져오기
    SpriteRenderer fadeSprite = fadePanel.GetComponent<SpriteRenderer>();
    
    if (fadeSprite == null)
    {
        Debug.LogError("FadePanel에 SpriteRenderer가 없습니다.");
        yield break;
    }

    // 페이드 인 시작 (투명한 상태에서 흰색으로)
    fadeSprite.color = new Color(1, 1, 1, 0);  // 시작 색상 설정 (투명)
    yield return fadeSprite.DOFade(1, fadeDuration).WaitForCompletion();  // 흰색으로 페이드

    // 씬 전환
    SceneManager.LoadScene(sceneName);
}

}