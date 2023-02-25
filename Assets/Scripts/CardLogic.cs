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

    public Image ImageSlot;
    public TextMeshProUGUI DescSlot;
    public TextMeshProUGUI LeftChoiceSlot;
    public TextMeshProUGUI RightChoiceSlot;

    public TextMeshProUGUI StatTextIT;
    public TextMeshProUGUI StatTextHR;
    public TextMeshProUGUI StatTextAcc;
    public TextMeshProUGUI StatTextMarketing;

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
        Debug.Log(pos);
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

            LoadCardData(CardData);
        }
    }

    public SOCardValues GetCardData()
    {
        int cardNumber = Random.Range(0, 3);
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
            Debug.Log("Stats" + i + " " + Stats[i]);
        }
        return Stats;
    }

    public int[] UpdateStatsRight(SOCardValues CardValues)
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i] += CardValues.rightStats[i];
        }
        return Stats;
    }

    public void UpdateStatsDisplay(int[] stats)
    {
        StatTextIT.text = stats[0].ToString();
        StatTextHR.text = stats[1].ToString();
        StatTextAcc.text = stats[2].ToString();
        StatTextMarketing.text = stats[3].ToString();
    }

    public void Start()
    {
        // Debug.Log(ImageSlot.name);
        // Debug.Log("Start!");
        // GetCardData();
        LoadCardData(GetCardData());
        for(int i = 0; i < Stats.Length; i++)
        {
            Stats[i] = 50;
        }
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
