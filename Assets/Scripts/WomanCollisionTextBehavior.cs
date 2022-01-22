using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanCollisionTextBehavior : MonoBehaviour
{
    public GameObject textUI;
    private KindPasserbyBehavior kindPasserbyBehavior;

    void Start()
    {
        kindPasserbyBehavior = GetComponent<KindPasserbyBehavior>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (!kindPasserbyBehavior.interacted)
            {
                textUI.GetComponent<TextScript>().ShowText();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        textUI.GetComponent<TextScript>().HideText();
    }
}
