using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;

    public string ending;


    private SOEndings endingRes;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        
    }

    public void SaveEnding()
    {
        GameLogic.Instance.ending = ending;
    }




}
