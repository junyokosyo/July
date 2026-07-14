using System;
using UnityEngine;
using System.Collections;
using TMPro;
/// <summary>
/// タイプライター風に文字を表示するクラス。1文字ずつ表示する。
/// </summary>
public class TypewriterText : MonoBehaviour
{
    [SerializeField] private TMP_Text textUI;
    [SerializeField] private float charInterval = 0.03f;
    private Coroutine currentRoutine;

    public bool IsTyping { get; private set; }

    /// <summary>1文字表示されるたびに発火。表示された文字を運ぶ。</summary>
    public event Action<char> OnCharTyped;

    public void ShowLine(string fullText)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(TypeRoutine(fullText));
    }

    private IEnumerator TypeRoutine(string fullText)
    {
        IsTyping = true;
        textUI.text = "";

        foreach (char c in fullText)
        {
            textUI.text += c;
            OnCharTyped?.Invoke(c);
            yield return new WaitForSeconds(charInterval);
        }

        IsTyping = false;
    }
}
