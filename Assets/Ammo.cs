using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public GameObject lid;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Gun gun = other.GetComponent<Gun>();
        if (gun)
        {
            gun.ammoRes = gun.ammoMax;
            gun.ammoMag = 30;
            gun.bombCurrent = gun.bombMax;
            gun.UpdateGrenadeUI();
            Destroy(gameObject);
            Destroy(lid);
        }
    }
}
