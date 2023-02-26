using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    public AudioSource pik;
    public GameObject animation;
    SceneChanger sceneChanger;
    Animator anim;

    public SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<SceneChanger>();
        textComponent.text = string.Empty;
        StartDialogue();
        spriteRenderer.sprite = Resources.Load<Sprite>($"Narrative/{2}");
        anim = animation.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }

            switch (index)
            {
                case 1:
                    spriteRenderer.sprite = Resources.Load<Sprite>($"Narrative/{1}");
                break;
                case 4:
                    spriteRenderer.sprite = Resources.Load<Sprite>($"Narrative/{3}");
                break;
                case 9:
                    spriteRenderer.sprite = Resources.Load<Sprite>($"Narrative/{4}");
                break;
                case 12:
                    spriteRenderer.sprite = Resources.Load<Sprite>($"Narrative/{5}");
                    break;
                default:
                break;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    
    IEnumerator TypeLine()
    {
        foreach (char line in lines[index].ToCharArray())
        {
            textComponent.text += line;
            yield return new WaitForSeconds(textSpeed);
            pik.Play();
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            sceneChanger.LoadNextLevel();
            anim.Play("anim_end");
            //LoadNextLevel();
        }
    }
}
