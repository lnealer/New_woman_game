using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBoxBehavior : MonoBehaviour
{
    public Transform bubble;
    public TextMeshProUGUI popUpText;
    //public Animator animator;
    public GameObject character;



    public static GameObject Create(Transform parent, Vector3 localPos, string text)
    {
        Transform bubblePrefab = GameObject.Find("GameAssets").GetComponent<GameAssetsScript>().chatBubble;
        Transform chatBubbleTransform = Instantiate(bubblePrefab, parent);
        chatBubbleTransform.localPosition = localPos;
        chatBubbleTransform.GetComponent<ChatBoxBehavior>().PopUp(text);
        return chatBubbleTransform.gameObject;
    }

    void Awake()
    {
        bubble = transform.Find("Bubble");
        ////popUpText = transform.Find("Text");
    }

    public void PopUp(string text)
    {
        popUpText.SetText(text);
        //popUpText.text = text;
        //GetComponent<Renderer>().enabled = true;
        //bubble.SetActive(true);
        //animator.SetTrigger("pop");
    }

    public void Hide()
    {
        Destroy(gameObject, 4f);
        //GetComponent<Renderer>().SetEnabled = false;   
    }

}
