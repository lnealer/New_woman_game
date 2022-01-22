using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionDisplayScript : MonoBehaviour
{
    public GameObject beggingInstructions;
    public GameObject womanInstructions;
    public GameObject levelInstructions;
    public GameObject suitorText;
    public GameObject endNarrative;
    public GameObject thiefText;
    private GameObject currentInstruction;
    public GameObject exhaustionInstructions;

    private bool exhausted;
    private float exhaustedTimer;
    private float exhaustedInterval = 2f;
    void Start()
    {
        currentInstruction = levelInstructions;
    }

    void Update()
    {
        exhaustedTimer+= Time.deltaTime;
        if (GetComponent<HealthBar>().health <= 0)
        {
            currentInstruction.gameObject.GetComponent<TextScript>().HideText();
            //exhaustionInstructions.GetComponent<TextScript>().ShowText();
            currentInstruction = exhaustionInstructions;
            currentInstruction.GetComponent<TextScript>().ShowText();
            exhaustedTimer = 0;
            exhausted =true;
        }

        if (exhausted & exhaustedTimer >= exhaustedInterval)
        {
            exhausted = false;
           currentInstruction.gameObject.GetComponent<TextScript>().HideText();
        }
        //currentInstruction.gameObject.GetComponent<TextScript>().ShowText();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name == "LevelInstructions")
        {
            //levelInstructions.GetComponent<TextScript>().ShowText();
            currentInstruction.gameObject.GetComponent<TextScript>().HideText();   
            currentInstruction = levelInstructions;
            currentInstruction.GetComponent<TextScript>().ShowText();
        }
        if (collider.gameObject.name == "EndNarrative")
        {
            //endNarrative.GetComponent<TextScript>().ShowText();
             currentInstruction.gameObject.GetComponent<TextScript>().HideText();  
            currentInstruction = endNarrative;    
            currentInstruction.GetComponent<TextScript>().ShowText();
        }
        else if (collider.gameObject.tag == "Woman")
        {
            if (!collider.gameObject.GetComponent<KindPasserbyBehavior>().interacted)
            {
               // beggingInstructions.GetComponent<TextScript>().ShowText();
                currentInstruction.gameObject.GetComponent<TextScript>().HideText();  
                currentInstruction = beggingInstructions;
                currentInstruction.GetComponent<TextScript>().ShowText();
            }
            else
            {
                //currentInstruction.gameObject.GetComponent<TextScript>().HideText();
                //womanInstructions.GetComponent<TextScript>().ShowText();
                 currentInstruction.gameObject.GetComponent<TextScript>().HideText();  
                currentInstruction = womanInstructions;
                currentInstruction.GetComponent<TextScript>().ShowText();
            }
        }
        else if (collider.gameObject.tag == "Suitor")
        {
           // suitorText.GetComponent<TextScript>().ShowText();
            currentInstruction.gameObject.GetComponent<TextScript>().HideText();  
            currentInstruction = suitorText;
            currentInstruction.GetComponent<TextScript>().ShowText();
        }
        else if (collider.gameObject.tag == "Thief")
        {
            if (Vector2.Distance(transform.position, collider.gameObject.transform.position) < 1f)
            {
               // thiefText.GetComponent<TextScript>().ShowText();
                currentInstruction.gameObject.GetComponent<TextScript>().HideText();  
                currentInstruction = thiefText;
                currentInstruction.GetComponent<TextScript>().ShowText();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        currentInstruction.gameObject.GetComponent<TextScript>().HideText();
    }
}
