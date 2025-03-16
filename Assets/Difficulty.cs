using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty difficultyScript;
    public int diff = 2;
    void Start()
    {

    }

    void Update()
    {
        
    }

    private void Awake()
    {
        if (difficultyScript == null)
        {
            DontDestroyOnLoad(gameObject);
            difficultyScript = this;
        }
        else if (difficultyScript != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetEasy()
    {
        diff = 1;
    }

    public void SetNormal()
    {
        diff = 2;
    }

    public void SetHard()
    {
        diff = 3;
    }
}
