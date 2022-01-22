using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dignity : MonoBehaviour
{
    public Image dignityBar;
    public float dignity = 100;

    public void LoseDignity(float dignityLost)
    {
        if (dignity <= 0)
        {
            return;
        }
        // reduce dignity
        dignity -= dignityLost;

        // update UI dignityBar
        dignityBar.fillAmount = dignity / 100;

        if (dignity <= 0)    
        {
            Debug.Log("you died of lost dignity");
        }
        
    }


    public void GainDignity(float dignityGained)
    {
        if (dignity >= 100)
        {
            return;
        }
        // increase dignity
        dignity += dignityGained;

        // update UI dignityBar
        dignityBar.fillAmount = dignity / 100;

    }
}
