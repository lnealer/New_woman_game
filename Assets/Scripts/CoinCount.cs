using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public Image[] coins;
    public int coinsCollected;

    // normal (not blacked out) coin color 
    private Color32 goldCoinColor = new Color32(156, 132, 64, 255);

    // hidden (blacked out) coin color 
    private Color32 hiddenCoinColor = new Color32(56, 56, 56, 255);

    public void LostCoin()
    {
        if (coinsCollected > 0)
        {
            // decrment number of coins collected
            coinsCollected--;
            // hide/black out one coin
            coins[coinsCollected].GetComponent<Image>().color = hiddenCoinColor;
        }

    }
    public void GetCoin(GameObject coin)
    {
        if (coinsCollected < 4)
        {
            Debug.Log("Got coin");
            Destroy(coin);
            // Incease number of coins collected
            coinsCollected++;
            // fill in one of the coins
            coins[coinsCollected-1].GetComponent<Image>().color = goldCoinColor;
            FindObjectOfType<Dignity>().GainDignity(10); // increase player dignity by 10
        }
     }

         public void GetCoin()
    {
        if (coinsCollected < 4)
        {
            Debug.Log("Got coin");
            // Incease number of coins collected
            coinsCollected++;
            // fill in one of the coins
            coins[coinsCollected-1].GetComponent<Image>().color = goldCoinColor;
            FindObjectOfType<Dignity>().GainDignity(10); // increase player dignity by 10
        }
     }

     void OnTriggerEnter2D(Collider2D collider)
     {
         Debug.Log("Entered Collision: "+ collider.gameObject.name);
         if (collider.gameObject.tag == "Coin")
         {
             // collect coin and gain dignity 
            GetCoin(collider.gameObject);
         }
     }
}
