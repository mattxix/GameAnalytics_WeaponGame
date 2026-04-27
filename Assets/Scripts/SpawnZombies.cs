using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnZombies : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject expoZombiePrefab;
    public GameObject patrolScript;
    public Transform player;
    public Transform[] spawnLocations;
    public int zombiesKilledCount = 0;
    public TextMeshProUGUI zombieKillHud;

    public void SpawnZombie()
    {
        int randomIndex = Random.Range(0, spawnLocations.Length);
        int chanceOfExpoZom = Random.Range(0, 3);

        Transform spawnPoint = spawnLocations[randomIndex];
        if (chanceOfExpoZom == 0 || chanceOfExpoZom == 1)
        {
            GameObject newZombie = Instantiate(expoZombiePrefab, spawnPoint.position, spawnPoint.rotation);

            EnemyHealth respawnScript = newZombie.GetComponent<EnemyHealth>();
            //if (respawnScript != null)
               // respawnScript.spawner = this;

            PatrolEnemy ai = newZombie.GetComponent<PatrolEnemy>();
            if (ai != null)
                ai.target = player;

            ExpoZombie expo = newZombie.GetComponentInChildren<ExpoZombie>();
            if (expo != null)
            {
                if (expo != null)
                {
                    expo.healthScript = player.GetComponentInChildren<PlayerHealth>();
                    expo.spawner = this;
                }
                PlayerHealth ph = player.GetComponentInChildren<PlayerHealth>();
                if (ph != null)
                {
                    expo.healthScript = ph;
                    Debug.Log($"Expo zombie linked to {ph.gameObject.name}");
                }
                else
                {
                    Debug.LogWarning("PlayerHealth not found!");
                }
            }
            else
            {
                Debug.Log("Spawned zombie missing ExpoZombie component.");
            }
        }
        else
        {
            GameObject newZombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

            EnemyHealth respawnScript = newZombie.GetComponent<EnemyHealth>();
            //if (respawnScript != null)
               // respawnScript.spawner = this;

            PatrolEnemy ai = newZombie.GetComponent<PatrolEnemy>();
            if (ai != null)
                ai.target = player;
        }
    }

    public void upCount()
    {
        zombiesKilledCount++;
        zombieKillHud.text = $"{zombiesKilledCount}x";
        if (zombiesKilledCount >= 50)
        {
            SceneManager.LoadScene(2);
        }
    }
}
