using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class EnemyWeaponScript : MonoBehaviour
{
    public float weaponDamage = 100.0f;
    public float weaponRange = 200.0f;
    public float fireRate = 20.0f;
    public float nextFire = 0.0f;
    public AudioSource audioSource;
    public AudioClip gunFireClip;

    //public GameObject fireIcon;
    public GameObject fakeRaycast;

 


    [Header("Reload")]
    public Image reloadBar;
    public float fillSpeed = 1;


    void Start()
    {
        StartCoroutine(RepeatEveryTwoSeconds());
        //fireIcon.SetActive(false);
    }
    private void Update()
    {
        Debug.DrawRay(fakeRaycast.transform.position, transform.forward * weaponRange, Color.green);
    }

    IEnumerator RepeatEveryTwoSeconds()
    {
        while (true)
        {
            // Fire for 1 second
            float fireEndTime = Time.time + 1f;
            while (Time.time < fireEndTime)
            {
                FireShot();
                yield return null;
            }

            // Wait 1 second
            yield return new WaitForSeconds(1f);
        }
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(gunFireClip);
        RaycastHit hit;
        if (Physics.Raycast(fakeRaycast.transform.position, transform.forward, out hit, weaponRange))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                //apply damage
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(weaponDamage);
                Debug.Log(hit.collider.name);
            }
        }
        

    }

    public void FireShot()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + 1.0f / fireRate;
            
            StartCoroutine(Recoil());
            Shoot();
        }
    }


    IEnumerator Recoil()
    {
        reloadBar.fillAmount = 0f;
        while (reloadBar.fillAmount < 1f)
        {
            reloadBar.fillAmount += Time.deltaTime * fillSpeed;
            reloadBar.fillAmount = Mathf.Clamp01(reloadBar.fillAmount);
            yield return null;

        }
        
    }

}
