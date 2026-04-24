using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    public float weaponDamage = 100.0f;
    public float weaponRange = 200.0f;
    public float fireRate = 20.0f;
    public float nextFire = 0.0f;
    public Camera fpsCamera;
    public Animator anim;
    public AudioSource audioSource; 
    public AudioClip gunFireClip;

    [Header("Scope")]
    public Camera playerCam;
    public float aimFOV = 55f;
    public float normalFOV = 85f;
    public float fovSpeed = 10f;

    public MouseLook lookScript;

    private bool isAiming = false;


    [Header("Reload")]
    public Image reloadBar;
    public float fillSpeed = 1;


    void Update()
    {
        float targetFOV = isAiming ? aimFOV : normalFOV;
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetFOV, Time.deltaTime * fovSpeed);
    }
    public void Shoot()
    {
        audioSource.PlayOneShot(gunFireClip);
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, transform.forward, out hit, weaponRange,
            Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                //apply damage
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(weaponDamage);
                Debug.Log(hit.collider.name);
            }
        }
        
    }

    public void FireShot(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && Time.time >= nextFire)
        {
            nextFire = Time.time + 1.0f / fireRate;
            anim.SetTrigger("Shoot");
            StartCoroutine(Recoil());
            Shoot();
        }
    }

    public void AimIn(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isAiming = true;
            lookScript.SetAiming(true);
        }
        else if (ctx.canceled)
        {
            isAiming = false;
            lookScript.SetAiming(false);
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
        anim.SetTrigger("Shoot");





    }


    


}
