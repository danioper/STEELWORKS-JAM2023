using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardLogic : MonoBehaviour
{
    public Canvas canvas;
    public GameObject RightChoice;
    public GameObject LeftChoice;
    public int CardsCount;
    public int Stress;

    public Image ImageSlot;
    public TextMeshProUGUI DescSlot;
    public TextMeshProUGUI LeftChoiceSlot;
    public TextMeshProUGUI RightChoiceSlot;

    public Slider ITSlider;
    public Slider HRSlider;
    public Slider AccSlider;
    public Slider MarkSlider;

    public SpriteRenderer StressSprite;

    // Between 0 and 100
    public int[] Stats = new int[4] { 50, 50, 50, 50 };

    private Vector2 pos;
    private List<int> availableNums = new List<int>();


    public void OnMouseDrag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out pos);
        pos = new Vector2(pos.x, -185);
        transform.position = canvas.transform.TransformPoint(pos);
        // Debug.Log(pos);
    }

    public void OnMouseEndDrag()
    {
        // Debug.Log("End drag!");
        transform.position = canvas.transform.TransformPoint(new Vector2(0, -185));

        if (pos.x < -500f || pos.x > 500f)
        {
            SOCardValues CardData = GetCardData();

            if (pos.x < -500f) UpdateStatsDisplay(UpdateStatsLeft(CardData));
            if (pos.x > 500f) UpdateStatsDisplay(UpdateStatsRight(CardData));

            UpdateStressDisplay(UpdateStress());
            LoadCardData(CardData);
        }
    }

    public SOCardValues GetCardData()
    {
        var rand = new System.Random();
        int cardNumber = rand.Next(availableNums.Count);
        availableNums.Remove(cardNumber);
        // Debug.Log("Removed element: " + cardNumber);
        // Debug.Log("List Count: " + availableNums.Count);

        foreach(var card in availableNums)
        {
           // Debug.Log("Card: " + card);
        }

        string dir = "ScriptableObjects/" + cardNumber.ToString();
        return Resources.Load(dir) as SOCardValues;
    }

    public void LoadCardData(SOCardValues CardValues)
    {
        LeftChoice.SetActive(false);
        RightChoice.SetActive(false);
        if(CardValues == null)
        {
            Debug.Log("CardValues is null!");
        }
        ImageSlot.sprite = CardValues.cardSprite;
        DescSlot.text = CardValues.description;
        LeftChoiceSlot.text = CardValues.leftChoice;
        RightChoiceSlot.text = CardValues.rightChoice;
    }

    public int[] UpdateStatsLeft(SOCardValues CardValues)
    {
        for (int i = 0; i < Stats.Length; i++) {
           Stats[i] += CardValues.leftStats[i];
            // Debug.Log("Stats" + i + " " + Stats[i]);
        }
        return Stats;
    }

    public int[] UpdateStatsRight(SOCardValues CardValues)
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            // Stats[i] += CardValues.rightStats[i];
        }
        return Stats;
    }

    public void UpdateStatsDisplay(int[] stats)
    {
        ITSlider.value = Stats[0];
        HRSlider.value = Stats[1];
        AccSlider.value = Stats[2];
        MarkSlider.value = Stats[3];
    }

    public int UpdateStress()
    {
        bool isStressUpdated = false;
        for(int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i] <= 20 && !isStressUpdated )
            {
                Stress += 20;
                isStressUpdated = true;
                Debug.Log("Stress updated");
            }
        }
        return Stress;
    }

    public void UpdateStressDisplay(int stress)
    {
        Sprite newSprite;
        switch(stress)
        {
            case 40:
                newSprite = Resources.Load("/StressSprites/pasek_40_stres") as Sprite;
                StressSprite.sprite = newSprite;
                break;

            case 60:
                newSprite = Resources.Load("/StressSprites/pasek_60_stres") as Sprite;
                StressSprite.sprite = newSprite;
                break;

            case 80:
                newSprite = Resources.Load("/StressSprites/pasek_80_stres") as Sprite;
                StressSprite.sprite = newSprite;
                break;

            case 100:
                newSprite = Resources.Load("/StressSprites/pasek_100_stres") as Sprite;
                StressSprite.sprite = newSprite;
                break;

            default:
                newSprite = Resources.Load("/StressSprites/pasek_20_stres") as Sprite;
                StressSprite.sprite = newSprite;
                break;
        }
    }

    public void Start()
    {
        // Debug.Log(ImageSlot.name);
        // Debug.Log("Start");
        for (int i = 0; i < CardsCount; i++)
        {
            availableNums.Add(i);
        }

        Stress = 20;

        //for (int i = 0; i < CardsCount; i++)
        //{
        //    Debug.Log("List element: " + availableNums[i]);
        //}

        LoadCardData(GetCardData());
        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i] = 50;
        }
        UpdateStatsDisplay(Stats);
        
        Debug.Log("Stress:" + Stress);
        Sprite newSprite;
        newSprite = Resources.Load("StressSprites/pasek_20_stres") as Sprite;
        Debug.Log(newSprite.name);
        StressSprite.sprite = newSprite;
    }

    public void Update()
    {
        if(pos.x < -400f)
        {
            LeftChoice.SetActive(true);
        } else
        {
            LeftChoice.SetActive(false);
        }

        if(pos.x > 400f)
        {
            RightChoice.SetActive(true);
        } else
        {
            RightChoice.SetActive(false);
        }
    }
}
