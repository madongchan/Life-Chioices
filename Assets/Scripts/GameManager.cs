using System.Collections.Generic;
using SQLite;
using UnityEngine;
public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager GetInstance() {
        return Instance;
    }
    public GameObject Canvas_chaptgerNight;
    public CharacterStateSliderManager characterStateSlideManager;
    public CharacterStateManager characterStateManager;
    public ParticlesManager particlesManager;
    public CardManager cardManager;
    public StoryText textManager;

    private void Start() {
        characterStateSlideManager.Initialize();
        // 실제 게임은 아래 코드를 주석 해제 후 사용
        Canvas_chaptgerNight.SetActive(true);
        Canvas_chaptgerNight.GetComponent<NightChapterDissolveEffect>().StartDissolveEffect(cardManager.ShowCardSelection(1));
        //테스트 용으로 주석 처리
        //var action = cardManager.ShowCardSelection(1); // Action을 가져옴
        //action(); // Action을 실행

    }
}