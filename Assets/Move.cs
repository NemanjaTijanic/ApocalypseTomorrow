using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;

    public Light flashlight;
    public Text onOff;

    private void FixedUpdate()
    {
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(velocity.x, 0, velocity.y);
    }

    private void Update()
    {
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }

    private void UpdateUI()
    {
        if (flashlight.enabled)
        {
            onOff.text = "ON";
        }
        else
        {
            onOff.text = "OFF";
        }
    }
}
