using UnityEngine;
using System;

public class InGameEventManager : MonoBehaviour
{

    /// <summary>客が店に出現した時に発火。抽選済みの客データ(欲しい銃も確定済み)を運ぶ。</summary>
    public event Action<CustomerRuntimeData> OnCustomerAppear;

    /// <summary>プレイヤーがEを押して会話を開始した時に発火。対象の客データを運ぶ。</summary>
    public event Action<CustomerRuntimeData> OnDialogueStartRequested;

    /// <summary>会話の1行が表示される時に発火。タイプライター表示するテキスト本文を運ぶ。</summary>
    public event Action<string> OnDialogueLineShown;

    /// <summary>客の会話が全て終了した時に発火。</summary>
    public event Action OnDialogueFinished;


    /// <summary>プレイヤーが判定ボタンを押した時に発火。引数なし、「押された」という事実のみ。</summary>
    public event Action OnJudgePressed;

    /// <summary>判定処理が完了した時に発火。true=正解、false=不正解。</summary>
    public event Action<bool> OnJudgmentResult;


    /// <summary>ジャンプスケア等の演出パターンが決まった時に発火。どの演出を再生するかのIDを運ぶ。</summary>
    public event Action<string> OnJumpscareTriggered;

    /// <summary>客が店を去る時に発火。引数なし。</summary>
    public event Action OnCustomerExit;




    /// <summary>客の出現イベントを発火する。</summary>
    public void EmitCustomerAppear(CustomerRuntimeData data) => OnCustomerAppear?.Invoke(data);

    /// <summary>会話開始要求イベントを発火する。</summary>
    public void EmitDialogueStartRequested(CustomerRuntimeData data) => OnDialogueStartRequested?.Invoke(data);

    /// <summary>会話1行分の表示イベントを発火する。</summary>
    public void EmitDialogueLineShown(string line) => OnDialogueLineShown?.Invoke(line);

    /// <summary>会話終了イベントを発火する。</summary>
    public void EmitDialogueFinished() => OnDialogueFinished?.Invoke();

    /// <summary>判定ボタン押下イベントを発火する。</summary>
    public void EmitJudgePressed() => OnJudgePressed?.Invoke();

    /// <summary>判定結果イベントを発火する。</summary>
    public void EmitJudgmentResult(bool isCorrect) => OnJudgmentResult?.Invoke(isCorrect);

    /// <summary>ジャンプスケア発火イベントを発火する。</summary>
    public void EmitJumpscareTriggered(string jumpscareId) => OnJumpscareTriggered?.Invoke(jumpscareId);

    /// <summary>客の退場イベントを発火する。</summary>
    public void EmitCustomerExit() => OnCustomerExit?.Invoke();



    public static InGameEventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}