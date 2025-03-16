using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private bool detonated = false;
    public float damage = 100;
    public float delay = 3f;
    public float radius = 5f;

    public GameObject explosionEffect;
    private AudioSource source;
    public AudioClip explosionSound;

    void Start()
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        StartCoroutine(DetonationDelay());
    }

    void Update()
    {
    }

    private void Detonate()
    {
        detonated = true;
        Instantiate(explosionEffect, transform.position, transform.rotation);
        source.PlayOneShot(explosionSound);

        Collider[] others = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider c in others){
            Enemy e = c.GetComponent<Enemy>();
            if (e)
            {
                e.TakeDamage(damage);
            }
        }

        Destroy(gameObject);

    }

    IEnumerator DetonationDelay()
    {
        yield return new WaitForSeconds(delay);
        Detonate();
    }
}
