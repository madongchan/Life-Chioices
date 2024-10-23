using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class StoryText : MonoBehaviour
{
    public GameObject storyText;
    TextMeshPro textMeshPro;
    public TextAnimatorPlayer textAnimatorPlayer;

    private void Start()
    {
        textMeshPro = storyText.GetComponent<TextMeshPro>();
        textAnimatorPlayer = storyText.GetComponent<TextAnimatorPlayer>();
    }
    public void ShowText(int NextStoryIndex)
    {
        textAnimatorPlayer.ShowText(StoryDAO.GetStory(NextStoryIndex).Description);
    }
}
