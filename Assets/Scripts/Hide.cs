using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public GameObject canvas;
    private bool isHide = true;
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            isHide = !isHide;
            canvas.SetActive(isHide);
        }
    }
}
