using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameKeyScript : MonoBehaviour
{

    public float textTimer;
    public float textInterval = 7f;
    // Update is called once per frame
    void Update()
    {
        textTimer += Time.deltaTime;
        if (Input.GetButtonDown("KeyMappings"))
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            textTimer = 0;
        }
        if (Input.GetButtonUp("KeyMappings"))
        {
            GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (textTimer >= textInterval)
        {
             GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}
