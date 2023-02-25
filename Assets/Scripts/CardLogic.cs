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
    public int IT = 50;
    public int HR = 50;
    public int Acc = 50;
    public int Marketing = 50;

    private Vector2 pos;

    public void OnMouseDrag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out pos);
        pos = new Vector2(pos.x, -199);
        transform.position = canvas.transform.TransformPoint(pos);
        Debug.Log(pos);
    }

    public void OnMouseEndDrag()
    {
        // Debug.Log("End drag!");
        transform.position = canvas.transform.TransformPoint(new Vector2(0, -199));

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
        int cardNumber = Random.Range(1, 4);
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
        IT += CardValues.leftStats[0];
        HR += CardValues.leftStats[1];
        Acc += CardValues.leftStats[2];
        Marketing += CardValues.leftStats[3];

        return new int[] {IT, HR, Acc, Marketing};
    }

    public int[] UpdateStatsRight(SOCardValues CardValues)
    {
        IT += CardValues.rightStats[0];
        HR += CardValues.rightStats[1];
        Acc += CardValues.rightStats[2];
        Marketing += CardValues.rightStats[3];

        return new int[] { IT, HR, Acc, Marketing };
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
