using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public GameObject firstHorde;
    public GameObject secondHorde;
    public GameObject enemies;

    public Player player;
    public Gun gun;
    public Transform helicopter;
    public GameObject helicopterMsg;
    public float activateDistance = 3;

    public Text hordeText;
    private float timer = 3;

    public GameObject pauseUI;
    private bool pause = false;
    public GameObject victoryUI;
    private bool victory = false;
    public GameObject defeatUI;
    private bool defeat = false;

    void Start()
    {

    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            hordeText.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !victory && !defeat){
            Pause();
        }

        if (!player.questCompleted)
        {
            helicopterMsg.SetActive(false);
        }
        else
        {
            float dist = Vector3.Distance(helicopter.position, player.transform.position);
            helicopterMsg.SetActive(dist < activateDistance);
        }

        if(Input.GetKeyDown(KeyCode.E) && helicopterMsg.activeSelf && !victory && !defeat && player.questCompleted)
        {
            Victory();
        }

        if(player.health <= 0)
        {
            defeat = true;
            Defeat();
        }
    }

    public void Pause()
    {
        pause = !pause;
        gun.enabled = !pause;
        Time.timeScale = pause ? 0 : 1;
        pauseUI.SetActive(pause);
        CursorLock(pause);
    }

    public void Victory()
    {
        gun.enabled = false;
        victoryUI.SetActive(true);
        CursorLock(true);

        enemies.SetActive(false);
        player.healthUI.gameObject.SetActive(false);
    }

    public void Defeat()
    {
        gun.enabled = !defeat;
        defeatUI.SetActive(defeat);
        CursorLock(defeat);

        enemies.SetActive(false);
        player.healthUI.gameObject.SetActive(false);
    }

    private void CursorLock(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void FirstHordeActivate()
    {
        firstHorde.SetActive(true);
        hordeText.gameObject.SetActive(true);
        timer = 3;
    }

    public void SecondHordeActivate()
    {
        secondHorde.SetActive(true);
        hordeText.gameObject.SetActive(true);
        timer = 3;
    }
}
