using UnityEngine;
using System;
using System.Collections.Generic;

public class InGameEventManager : MonoBehaviour
{
    public static InGameEventManager Instance { get; private set; }

    //イベント定義
    //public event Action<CustomerData> OnCustomerData;


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
