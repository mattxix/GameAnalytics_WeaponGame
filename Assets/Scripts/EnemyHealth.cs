using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    public float enemyHealth = 100;
    public float currentHealth;
    public GameObject healthBarParent;
    public SpawnEnemy spawner;
    public AudioClip zombieHitClip;

    void Start()
    {
        currentHealth = enemyHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthBar.fillAmount = currentHealth / enemyHealth;
        }

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        if (zombieHitClip != null)
        {
            GameObject tempAudio = new GameObject("ZombieDeathSound");
            tempAudio.transform.position = transform.position;

            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = zombieHitClip;
            tempSource.spatialBlend = 1f; 
            tempSource.volume = 0.5f;
            tempSource.minDistance = 2f;  
            tempSource.maxDistance = 25f; 
            tempSource.rolloffMode = AudioRolloffMode.Linear;

            tempSource.Play();
            Destroy(tempAudio, zombieHitClip.length);

            
        }

        spawner.SpawnZombie();
        Destroy(healthBarParent);
        Destroy(gameObject);

        
    }
}
