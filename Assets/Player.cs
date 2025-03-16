using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public Image healthUI;
    public bool dead = false;

    public int questMax= 5;
    public int questCurrent = 0;
    public bool questCompleted;

    public Text questMaxText;
    public Text questCurrentText;
    void Start()
    {
        questCompleted = false;
    }

    void Update()
    {
        UpdateUI();

        if(questCurrent == questMax)
        {
            questCompleted = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        health -= damage;
        if(health <= 0)
        {
            dead = true;
        }
    }

    private void UpdateUI()
    {
        healthUI.rectTransform.localScale = new Vector3(health / maxHealth, 1, 1);

        questCurrentText.text = questCurrent.ToString();
        questMaxText.text = questMax.ToString();
    }

    public void ProgressQuest(int prog)
    {
        if(questCurrent < questMax)
        {
            questCurrent += prog;
        }
    }
}
