using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public void ShowText()
    {
        gameObject.SetActive(true);
    }

    public void HideText()
    {
        gameObject.SetActive(false);
    }
}
