using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="ScriptableObject/ChoiceCard")]

public class SOCardValues : ScriptableObject
{
    public Sprite cardSprite;
    public string description, leftChoice, rightChoice;
}
