using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CoinCount coinCount;
    private Dignity dignity;
    void Start()
    {
        coinCount = GameObject.FindObjectOfType<CoinCount>();
        dignity = GameObject.FindObjectOfType<Dignity>();
    }

    void Update()
    {
        if (dignity.dignity <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter2D()
    {
        if (coinCount.coinsCollected < 4)
        {
            SceneManager.LoadScene("GameOver");
        }   
        else 
        {
            SceneManager.LoadScene("Win");
        }
    }
}
