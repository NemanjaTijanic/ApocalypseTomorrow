using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField]
    Transform player;
    Vector2 currentLook;
    Vector2 delta;
    public float sensitivity = 1;
    public float smoothing = 2;

    private void Reset()
    {
        player = GetComponentInParent<Move>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 smoothness = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
        delta = Vector2.Lerp(delta, smoothness, 1 / smoothing);
        currentLook += delta;
        currentLook.y = Mathf.Clamp(currentLook.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-currentLook.y, Vector3.right);
        player.localRotation = Quaternion.AngleAxis(currentLook.x, Vector3.up);
    }
}
