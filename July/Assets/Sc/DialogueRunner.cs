using UnityEngine;


/// <summary>
/// 会話の進行を管理するクラス。カスタマーが出現した際に、DialogueLinesを順番に表示する。
/// </summary>
public class DialogueRunner : MonoBehaviour
{
    [SerializeField] private TypewriterText typewriter;

    private string[] lines;
    private int currentIndex;
    private void OnEnable()
    {
        InGameEventManager.Instance.OnCustomerAppear += HandleCustomerAppear;
    }

    private void OnDisable()
    {
        InGameEventManager.Instance.OnCustomerAppear -= HandleCustomerAppear;
    }

    private void HandleCustomerAppear(CustomerRuntimeData data)
    {
        lines = data.Template.DialogueLines.ToArray();
        currentIndex = 0;
        ShowCurrentLine();
    }

    /// <summary>クリックやキー入力で次の行へ進める想定。</summary>
    public void Advance()
    {
        if (typewriter.IsTyping)
        {
            // タイプ中にクリックされた場合は、タイプを即座に完了させる物を呼び出したい
            return;
        }

        currentIndex++;
        if (currentIndex >= lines.Length)
        {
            EndDialogue();
            return;
        }
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        string line = lines[currentIndex];
        typewriter.ShowLine(line);
        InGameEventManager.Instance.EmitDialogueLineShown(line);
    }

    private void EndDialogue()
    {
        InGameEventManager.Instance.EmitDialogueFinished();
        Debug.Log("会話終了");
    }
}
