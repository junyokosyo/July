using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 会話の進行を管理するクラス。カスタマーが出現した際に、DialogueLinesを順番に表示する。
/// </summary>
public class DialogueRunner : MonoBehaviour
{
    [SerializeField] private TypewriterText typewriter;
    [SerializeField] private InputActionReference interactAction;

    private string[] lines;
    private int currentIndex;
    private void OnEnable()
    {
        InGameEventManager.Instance.OnCustomerAppear += HandleCustomerAppear;
        interactAction.action.performed += OnInteract;
        interactAction.action.Enable();
    }

    private void OnDisable()
    {
        InGameEventManager.Instance.OnCustomerAppear -= HandleCustomerAppear;
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Advance();
        }
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
        if (lines == null) return;

        if (typewriter.IsTyping)
        {
            typewriter.Skip();
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
        lines = null;
        Debug.Log("会話終了");
    }
}
