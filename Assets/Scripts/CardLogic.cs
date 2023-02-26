using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
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
    public float Timer;
    public float TimeShow = 3f;

    public Image ImageSlot;
    public TextMeshProUGUI DescSlot;
    public TextMeshProUGUI LeftChoiceSlot;
    public TextMeshProUGUI RightChoiceSlot;

    public Slider ITSlider;
    public Slider HRSlider;
    public Slider AccSlider;
    public Slider MarkSlider;

    public GameObject[] Coms = new GameObject[4];
    public TextMeshProUGUI[] ComTexts = new TextMeshProUGUI[4];

    public SpriteRenderer StressSprite;
    public SpriteRenderer CashSprite;

    // Between 0 and 100
    public int[] Stats = new int[5] { 50, 50, 50, 50, 20 };

    private Vector2 pos;
    private List<int> availableNums = new List<int>();

    private int cardNumber;

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
            SOCardValues OldCardData = GetCardData();
            cardNumber = GetCardNumber();
            SOCardValues CardData = GetCardData();

            if (pos.x < -500f)
            {
                DisplayComs(GetLeftCommentsNumber(OldCardData), GetLeftComments(OldCardData));
                UpdateStatsDisplay(UpdateStatsLeft(CardData));
            }
            if (pos.x > 500f)
            {
                DisplayComs(GetRightCommentsNumber(OldCardData), GetRightComments(OldCardData));
                UpdateStatsDisplay(UpdateStatsRight(CardData));
            }

            UpdateStressDisplay(UpdateStress());
            LoadCardData(CardData);
        }
    }

    public int GetCardNumber()
    {
        var rand = new System.Random();
        int cardNumber = rand.Next(availableNums.Count);
        availableNums.Remove(cardNumber);
        return cardNumber;
    }

    public SOCardValues GetCardData()
    {
        // Debug.Log("Removed element: " + cardNumber);
        // Debug.Log("List Count: " + availableNums.Count);

        //foreach(var card in availableNums)
        //{
        //   // Debug.Log("Card: " + card);
        //}

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

    public int GetLeftCommentsNumber(SOCardValues CardValues)
    {
        return CardValues.leftComments.Length;
    }

    public int GetRightCommentsNumber(SOCardValues CardValues)
    {
        return CardValues.rightComments.Length;
    }

    public string[] GetLeftComments(SOCardValues CardValues)
    {
        return CardValues.leftComments;
    }
    public string[] GetRightComments(SOCardValues CardValues)
    {
        return CardValues.rightComments;
    }

    public int[] UpdateStatsLeft(SOCardValues CardValues)
    {
        for (int i = 0; i < Stats.Length; i++) {
           Stats[i] += CardValues.leftStats[i];
            // Debug.Log("Stats" + i + " " + Stats[i]);
        }
        string[] comments = CardValues.leftComments;

        //DisplayComs(commentsNumber, comments);
        //StartCoroutine(WaitAndDestroyComs());
        

        return Stats;
    }

    public int[] UpdateStatsRight(SOCardValues CardValues)
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i] += CardValues.rightStats[i];
        }

        int commentsNumber = CardValues.rightComments.Length;
        string[] comments = CardValues.rightComments;
        
        //DisplayComs(commentsNumber, comments);
        //StartCoroutine(WaitAndDestroyComs());

        return Stats;
    }

    public void DisplayComs(int commentsNumber, string[] comments)
    {
        for (int i = 0; i < commentsNumber; i++)
        {
            if (comments[i] != "")
            {
                Debug.Log("comment: " + i + ": " + comments[i]);
                Coms[i].SetActive(true);
                ComTexts[i].text = comments[i];
            }
        }
        StartCoroutine(WaitAndDestroyComs());
    }

    IEnumerator WaitAndDestroyComs()
    {
        yield return new WaitForSeconds(TimeShow);
        for (int i = 0; i < 4; i++)
        {
            Coms[i].SetActive(false);
        }

    }

    public void UpdateStatsDisplay(int[] stats)
    {
        ITSlider.value = stats[0];
        HRSlider.value = stats[1];
        AccSlider.value = stats[2];
        MarkSlider.value = stats[3];

        Sprite cashSprite;

        switch (stats[4])
        {
            case 20:
                cashSprite = Resources.Load<Sprite>("CashSprites/pasek_20_kasa");
                break;

            case 40:
                cashSprite = Resources.Load<Sprite>("CashSprites/pasek_40_kasa");
                break;

            case 60:
                cashSprite = Resources.Load<Sprite>("CashSprites/pasek_60_kasa");
                break;

            case 80:
                cashSprite = Resources.Load<Sprite>("CashSprites/pasek_80_kasa");
                break;

            case 100:
                cashSprite = Resources.Load<Sprite>("CashSprites/pasek_100_kasa");
                break;

            default:
                cashSprite = Resources.Load<Sprite>("CashSprites/pasek_20_kasa");
                break;
        }
        CashSprite.sprite = cashSprite;
    }

    public int UpdateStress()
    {
        bool isStressUpdated = false;
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i] <= 20 && !isStressUpdated)
            {
                Stress += 20;
                isStressUpdated = true;
                // Debug.Log("Stress updated: " + Stress);
            }
        }
        return Stress;
    }

    public void UpdateStressDisplay(int stress)
    {
        Sprite newSprite;
        // Debug.Log("Stress: " + stress);
        switch (stress)
        {
            case 20:
                newSprite = Resources.Load<Sprite>("StressSprites/pasek_20_stres");
                break;

            case 40:
                newSprite = Resources.Load<Sprite>("StressSprites/pasek_40_stres");
                break;

            case 60:
                newSprite = Resources.Load<Sprite>("StressSprites/pasek_60_stres");
                break;

            case 80:
                newSprite = Resources.Load<Sprite>("StressSprites/pasek_80_stres");
                break;

            case 100:
                newSprite = Resources.Load<Sprite>("StressSprites/pasek_100_stres");
                break;

            default:
                newSprite = Resources.Load<Sprite>("StressSprites/pasek_20_stres");
                break;
        }
        // Debug.Log("Sprite name: " + newSprite.name);
        StressSprite.sprite = newSprite;
    }

    public void Start()
    {
        // Debug.Log(ImageSlot.name);
        // Debug.Log("Start");
        for (int i = 0; i < CardsCount; i++)
        {
            availableNums.Add(i);
        }
        cardNumber = GetCardNumber();
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
