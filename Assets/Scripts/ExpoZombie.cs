using System.Collections;
using UnityEngine;

public class ExpoZombie : MonoBehaviour
{
    public GameObject zombie;             
    public PlayerHealth healthScript;      
    private bool playerInRange = false;
    public SpawnZombies spawner;
    public AudioClip zombieExpoClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            StartCoroutine(StartFuse());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator StartFuse()
    {
        
        yield return new WaitForSeconds(0.3f);

        
        if (playerInRange && healthScript != null)
        {
            healthScript.TakeExpoDamage();
        }
        if (spawner != null)
        {
            spawner.SpawnZombie();
        }
        if (zombieExpoClip != null)
        {
            
            GameObject tempAudio = new GameObject("ZombieExplodeSound");
            tempAudio.transform.position = transform.position;

            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = zombieExpoClip;
            tempSource.spatialBlend = 1f;
            tempSource.volume = 1f;
            tempSource.minDistance = 2f;  
            tempSource.maxDistance = 25f; 
            tempSource.rolloffMode = AudioRolloffMode.Linear;

            tempSource.Play();
            Destroy(tempAudio, zombieExpoClip.length);

            Debug.Log("playing audio");
        }

        Destroy(zombie);
    }
}
