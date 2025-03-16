using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstHordeTrigger : MonoBehaviour
{
    public GM gm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            gm.FirstHordeActivate();
            Destroy(gameObject);
        }
    }
}
