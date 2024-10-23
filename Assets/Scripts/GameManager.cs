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
    public CharacterStateSliderManager characterStateManager;
    public CardManager cardManager;
    public StoryText textManager;

    private void Start() {
        Canvas_chaptgerNight.SetActive(true);
        cardManager.ShowCardSelection(1);
    }
}