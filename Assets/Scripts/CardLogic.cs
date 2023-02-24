using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardLogic : MonoBehaviour
{
    public Canvas canvas;
    public GameObject RightChoice;
    public GameObject LeftChoice;

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
        // Debug.Log(pos);
    }

    public void OnMouseEndDrag(BaseEventData data)
    {
        // Debug.Log("End drag!");
        transform.position = canvas.transform.TransformPoint(new Vector2(0, -199));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("collision");
        if(collision.gameObject.name == "LeftChoiceTrigger")
        {
            LeftChoice.SetActive(true);
        }

        if (collision.gameObject.name == "RightChoiceTrigger")
        {
            RightChoice.SetActive(true);
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("collision");
        if (collision.gameObject.name == "LeftChoiceTrigger")
        {
            LeftChoice.SetActive(false);
        }

        if (collision.gameObject.name == "RightChoiceTrigger")
        {
            RightChoice.SetActive(false);
        }

    }
}
