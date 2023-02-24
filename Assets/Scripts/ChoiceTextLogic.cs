using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTextLogic : MonoBehaviour
{
    public GameObject LeftTextField;
    public GameObject RightTextField;


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D leftCollider = LeftTextField.GetComponent<BoxCollider2D>();
        BoxCollider2D rightCollider = RightTextField.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
