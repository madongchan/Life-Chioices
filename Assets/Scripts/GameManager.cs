using Febucci.UI.Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int num = 5;
    public CardManager cardManager;
    public TextDataManager textDataManager;
    public CharaterStateManager characterStateManager;
    public JSONTextDisplay jsonTextDisplay;

    void Awake()
    {
        TAnimBuilder.InitializeGlobalDatabase();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Find를 사용하여 컴포넌트 찾기
        cardManager = FindObjectOfType<CardManager>();
        characterStateManager = FindObjectOfType<CharaterStateManager>();
        jsonTextDisplay = FindObjectOfType<JSONTextDisplay>();
        textDataManager = FindObjectOfType<TextDataManager>();

        // 컴포넌트를 찾았는지 확인 및 로그 출력
        Debug.Log($"CardManager found: {cardManager != null}");
        Debug.Log($"CharaterStateManager found: {characterStateManager != null}");
        Debug.Log($"JSONTextDisplay found: {jsonTextDisplay != null}");
        Debug.Log($"TextDataManager found: {textDataManager != null}");

        // null 체크 후 메서드 호출
        if (textDataManager != null)
        {
            textDataManager.LoadTextData("textData.json", "cardTextData.json");
        }
        else
        {
            Debug.LogError("TextDataManager not found in the scene.");
        }

        if (characterStateManager != null)
        {
            characterStateManager.Initialize();
        }
        else
        {
            Debug.LogError("CharaterStateManager not found in the scene.");
        }
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }
}