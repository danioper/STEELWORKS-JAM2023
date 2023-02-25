using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Canvas canvas;
    public GameObject Card;
    public SOCardValues CardValues;

    private Sprite cardSprite;
    private string desc, leftChoice, rightChoice;


    // Start is called before the first frame update
    void Start()
    {   
        Debug.Log("Start!");
        CardValues = ScriptableObject.CreateInstance<SOCardValues>();
        cardSprite = Card.transform.GetChild(0).gameObject.GetComponent<Sprite>();
        //cardSprite.S = CardValues.cardSprite;
    }

    
}
