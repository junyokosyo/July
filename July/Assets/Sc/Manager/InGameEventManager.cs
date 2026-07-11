using UnityEngine;
using System;
using System.Collections.Generic;

public class InGameEventManager : MonoBehaviour
{
    public static InGameEventManager Instance { get; private set; }

    //イベント定義
    public event Action<CustomerData> OnCustomerAppear;
    public event Action<DialogueLine> OnDialogueLineShown;
    public event Action<string> OnItemRequested; 
    public event Action<string> OnItemPlacedOnCounter;
    public event Action OnJudgePressed;
    public event Action<bool> OnJudgmentResult;
    public event Action<string> OnJumpscareTriggered;
    public event Action OnCustomerExit;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
