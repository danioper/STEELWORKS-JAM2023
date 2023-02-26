using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndingLogic : MonoBehaviour
{
    public GameLogic gameLogic;
    public TextMeshProUGUI TextField;

    private SOEndings endingRes;

    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("gameplayManager").GetComponent<GameLogic>();
        //    string dir = "ScriptableObjects/" + gameLogic.ending;
        //    endingRes = Resources.Load(dir) as SOEndings;
        //    Debug.Log(endingRes.ToString());
        //    TextField.text = endingRes.Description;

        switch (gameLogic.ending) {
            case "acc_high":
                TextField.text = "The out-of-control accountants took advantage of the holes in the documents and seized a large part of the company's finances.   Your funds were also acquired by them by which you lost all your assets.";
                break;

            case "acc_low":
                TextField.text = "Due to accounting inaccuracies, the company failed to pay a significant portion of taxes to you.  You became a scapegoat and were fired from your job and the court sentenced you to years in prison.";
                break;

            case "HR_high":
                TextField.text = "HR having your support, climbed the pyramid of positions managed by the company. They concluded that everyone was slow to work and fired all departments.  You also worked too little and were disciplined.";
                break;

            case "HR_low":
                TextField.text = "With no HR staff, employees from other departments got lazy and did not finish the product on time.  The blame for this was placed on you and you were also fired fairly quickly.";
                break;

            case "IT_high":
                TextField.text = "By supporting IT heavily, they rose to prominence. No one quite understood what they did until they finally took over all the company's technology for themselves cutting off the rest of the employees from it.  They can't help it, you've been removed as a manager without any resources.";
                break;

            case "IT_low":
                TextField.text = "Due to the lack of IT staff, the company's security was breached and all the funds it had were stolen.  They can't help it, you've been removed as a manager without any resources.";
                break;

            case "mark_high":
                TextField.text = "The marketing people in the company practically went crazy and looked everywhere for a way to advertise the company.   Finally they came up with the idea to send your private photos that showed you not very favorably. A scandal broke out and you were fired from the company.";
                break;

            case "mark_low":
                TextField.text = "Because of the pouncing on marketig, customers knew little about the company. In fact, they knew nothing at all, which the competition took advantage of, and at some point everyone hated PHC.   To alleviate the affair you became the scapegoat and the board fired you from the company.";
                break;

            case "no_cash":
                TextField.text = "You spent the last of the company's money. Unfortunately, there wasn't enough left to polish the next product, and PHC went bankrupt.  You were purchased by the Love Platformers Company, and they set about producing a new platform game.";
                break;

            case "stress":
                TextField.text = "I'm beginning to understand why my predecessors always end the same way, they just want peace and quiet  25 year... now I know what part of my life this age will be. ";
                break;

            case "victory":
                TextField.text = "pupa";
                break;

            default:
                TextField.text = "dupa";
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
