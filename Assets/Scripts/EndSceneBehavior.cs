using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneBehavior : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("Start");
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
