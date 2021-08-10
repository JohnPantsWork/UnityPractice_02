using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }
    }

    void Quit()
    {
        Application.Quit();
        Debug.Log("We pushed quit");
    }
}
