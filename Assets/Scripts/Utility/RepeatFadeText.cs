using UnityEngine;
using Febucci.UI;
using System.Collections;
using TMPro;

public class RepeatFadeText : MonoBehaviour {
    public TextAnimatorPlayer textAnimatorPlayer;
    string textToShow = "<fade>당신의 인생, 당신의 선택. 이제 그 첫 걸음을 내딛어 보세요.</fade>";

    void Start() {
        StartCoroutine(RepeatTextCoroutine());
    }

    IEnumerator RepeatTextCoroutine() {
        while (true) {
            textAnimatorPlayer.ShowText(textToShow);
            yield return new WaitForSeconds(textToShow.Length * 0.115f);
            textAnimatorPlayer.StopShowingText();
        }
    }
}