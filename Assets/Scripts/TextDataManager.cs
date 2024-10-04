using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TextDataManager : MonoBehaviour
{
    private Dictionary<string, string> textData;
    private Dictionary<string, string> cardTextData;

    public void LoadTextData(string textDataFilePath, string cardTextDataFilePath)
    {
        textDataFilePath = Path.Combine(Application.streamingAssetsPath, "textData.json");
        cardTextDataFilePath = Path.Combine(Application.streamingAssetsPath, "cardTextData.json");

        // textData.json 파일 불러오기
        if (File.Exists(textDataFilePath))
        {
            string textDataJson = File.ReadAllText(textDataFilePath);
            TextDataWrapper loadedTextData = JsonUtility.FromJson<TextDataWrapper>(textDataJson);
            Debug.Log($"loadedTextData: {loadedTextData}");
            if (loadedTextData != null && loadedTextData.texts != null)
            {
                textData = new Dictionary<string, string>();
                foreach (TextData text in loadedTextData.texts)
                {
                    textData[text.id] = text.content;
                }
            }
            else
            {
                Debug.LogError("textData.json 파일에서 데이터를 파싱하는데 실패했습니다.");
            }
        }
        else
        {
            Debug.LogError("textData.json 파일을 찾을 수 없습니다.");
        }

        // cardTextData.json 파일 불러오기 (비슷한 방식으로 수정 필요)
        if (File.Exists(cardTextDataFilePath))
        {
            string cardTextDataJson = File.ReadAllText(cardTextDataFilePath);
            CardTextDataWrapper loadedCardTextData = JsonUtility.FromJson<CardTextDataWrapper>(cardTextDataJson);

            if (loadedCardTextData != null && loadedCardTextData.texts != null)
            {
                cardTextData = new Dictionary<string, string>();
                foreach (CardTextData cardText in loadedCardTextData.texts)
                {
                    cardTextData[cardText.id] = cardText.content;
                }
            }
            else
            {
                Debug.LogError("cardTextData.json 파일에서 데이터를 파싱하는데 실패했습니다.");
            }
        }
        else
        {
            Debug.LogError("cardTextData.json 파일을 찾을 수 없습니다.");
        }
    }
    // 카드 ID로 텍스트 찾기
    public string GetCardText(string cardId)
    {
        if (cardTextData.ContainsKey(cardId))
        {
            return cardTextData[cardId];
        }
        return "텍스트를 찾을 수 없습니다.";
    }
    // 텍스트 ID로 텍스트 찾기
    public string GetText(string textId)
    {
        if (textData.ContainsKey(textId))
        {
            return textData[textId];
        }
        return "텍스트를 찾을 수 없습니다.";
    }
    // 카드 ID로 카드 찾기
    public string GetCard(string cardId)
    {
        if (cardTextData.ContainsKey(cardId))
        {
            return cardTextData[cardId];
        }
        return "카드를 찾을 수 없습니다.";
    }
}
// JSON 구조에 맞는 클래스들
[System.Serializable]
public class TextData
{
    public string id;
    public string content;
}

[System.Serializable]
public class CardTextData
{
    public string id;
    public string content;
}

[System.Serializable]
public class TextDataWrapper
{
    public List<TextData> texts;
}

[System.Serializable]
public class CardTextDataWrapper
{
    public List<CardTextData> texts;
}