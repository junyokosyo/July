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
    private string currentFullText;

    public bool IsTyping { get; private set; }

    /// <summary>1文字表示されるたびに発火。表示された文字を運ぶ。</summary>
    public event Action<char> OnCharTyped;



    /// <summary>タイプ中の行を即座に全文表示して完了させる。</summary>
    public void Skip()
    {
        if (!IsTyping) return;

        if (currentRoutine != null) StopCoroutine(currentRoutine);
        textUI.text = currentFullText; // 一気に全文をセット
        IsTyping = false;
    }


    // TypewriterText に追加
    /// <summary>表示中のテキストを空にする。タイプ中なら停止する。</summary>
    public void Clear()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        textUI.text = "";
        IsTyping = false;
    }

    public void ShowLine(string fullText)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentFullText = fullText;
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
