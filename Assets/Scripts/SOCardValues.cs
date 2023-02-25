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

    // IT, HR, Acc, Marketing
    public int[] leftStats = new int[4];
    public int[] rightStats = new int[4];

    public string[] leftComments = new string[4];
    public string[] rightComments= new string[4];
}
