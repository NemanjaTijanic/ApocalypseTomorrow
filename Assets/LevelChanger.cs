﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelChanger : MonoBehaviour
{
    public Image panel;
    public float delay = 2;

    void Start()
    {
        panel.gameObject.SetActive(true);
        panel.DOFade(0, delay);
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(Load(level));
    }

    IEnumerator Load(int level)
    {
        Time.timeScale = 1;
        panel.DOFade(1, delay);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(level);
    }
}
