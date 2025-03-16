using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    public float fireRate = 0.2f;
    public bool canFire = true;
    public float force = 500;

    public int ammoMax = 180;
    public int ammoRes = 180;
    public int ammoMag = 30;
    
    public Transform camera;
    public Transform gun;
    public Transform arms;

    public AudioClip shotAudio;
    public AudioClip shellAudio;
    public AudioClip reloadAudio;
    private AudioSource audioSource;

    public ParticleSystem shootParticle;

    public Text magText;
    public Text ammoText;

    public float damage = 20;

    public GameObject bomb;
    public Transform thrower;
    public int bombMax = 2;
    public int bombCurrent = 1;
    public float bombCooldown = 2f;
    public float throwForce = 20f;
    public bool canBomb = true;

    public Image bombImageOne;
    public Image bombImageTwo;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateGrenadeUI();
    }

    void Update()
    {
        magText.text = ammoMag.ToString();
        ammoText.text = ammoRes.ToString();
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowBomb();
        }
    }

    private void Shoot()
    {
        if(canFire && ammoMag == 1)
        {
            ammoMag--;

            audioSource.PlayOneShot(shotAudio);
            audioSource.PlayOneShot(shellAudio, 0.1f);

            gun.DOPunchPosition(new Vector3(0, 0, -0.1f), 0.2f, 0);
            arms.DOPunchPosition(new Vector3(0, 0, -0.1f), 0.2f, 0);

            canFire = false;
            StartCoroutine(Reload());

            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, 200))
            {
                Enemy e = hit.collider.GetComponent<Enemy>();
                if (e)
                {
                    e.TakeDamage(damage);
                }

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForceAtPosition(-hit.normal * force, hit.point);
                }
            }
        }
        else if (canFire && ammoMag > 1)
        {
            ammoMag--;

            audioSource.PlayOneShot(shotAudio);
            audioSource.PlayOneShot(shellAudio, 0.1f);

            shootParticle.Play();

            gun.DOPunchPosition(new Vector3(0, 0, -0.1f), 0.2f, 0);
            arms.DOPunchPosition(new Vector3(0, 0, -0.1f), 0.2f, 0);

            canFire = false;
            StartCoroutine(RateOfFire());

            RaycastHit hit;
            if(Physics.Raycast(camera.position, camera.forward, out hit, 200))
            {
                Enemy e = hit.collider.GetComponent<Enemy>();
                if (e)
                {
                    e.TakeDamage(damage);
                }

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForceAtPosition(-hit.normal * force, hit.point);
                }
            }
        }   
    }

    private void ThrowBomb()
    {
        if(canBomb && bombCurrent > 0)
        {
            GameObject bomba = Instantiate(bomb, thrower.transform.position, thrower.transform.rotation);
            Rigidbody rig = bomba.GetComponent<Rigidbody>();
            rig.AddForce(thrower.forward * throwForce, ForceMode.VelocityChange);

            canBomb = false;
            bombCurrent--;
            UpdateGrenadeUI();
            StartCoroutine(GrenadeCooldown());
        }
    }

    IEnumerator RateOfFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;

        shootParticle.Stop();
    }

    IEnumerator GrenadeCooldown()
    {
        yield return new WaitForSeconds(bombCooldown);
        canBomb = true;
    }

    IEnumerator Reload()
    {
        audioSource.PlayOneShot(reloadAudio);
        yield return new WaitForSeconds(2);
        canFire = true;

        if(ammoRes == 30)
        {
            ammoRes = 0;
            ammoMag = 30;
        }
        else if(ammoRes > 30)
        {
            ammoRes -= 30;
            ammoMag = 30;
        }
        else if(ammoRes > 0)
        {
            ammoMag = ammoRes;
            ammoRes = 0;
        }
    }

    public void UpdateGrenadeUI()
    {
        if(bombCurrent == 2)
        {
            bombImageOne.gameObject.SetActive(true);
            bombImageTwo.gameObject.SetActive(true);
        }
        else if(bombCurrent == 1)
        {
            bombImageOne.gameObject.SetActive(true);
            bombImageTwo.gameObject.SetActive(false);
        }
        else
        {
            bombImageOne.gameObject.SetActive(false);
            bombImageTwo.gameObject.SetActive(false);
        }
    }
}
