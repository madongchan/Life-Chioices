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

    public CharacterStateSliderManager characterStateManager;

    private void Start()
    {
        
    }
}