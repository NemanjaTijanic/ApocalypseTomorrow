using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM : MonoBehaviour
{
    public Toggle easy;
    public Toggle normal;
    public Toggle hard;

    void Start()
    {
        
    }

    void Update()
    {
        if (easy.isOn)
        {
            Difficulty.difficultyScript.diff = 1;
        }
        else if (normal.isOn)
        {
            Difficulty.difficultyScript.diff = 2;
        }
        else if (hard.isOn)
        {
            Difficulty.difficultyScript.diff = 3;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
