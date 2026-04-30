using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject zombiePrefab;
    
    public GameObject patrolScript;
    public Transform player;
    public Transform[] spawnLocations;
    

    [Header("WeaponArray")]
    public GameObject[] Weapons;

    public void SpawnZombie()
    {
        int randomIndex = Random.Range(0, spawnLocations.Length);
        int randomWeapon = Random.Range(0, Weapons.Length);
        Transform spawnPoint = spawnLocations[randomIndex];

        GameObject newZombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        EnemyHealth respawnScript = newZombie.GetComponent<EnemyHealth>();
        if (respawnScript != null)
            respawnScript.spawner = this;

        PatrolEnemy ai = newZombie.GetComponent<PatrolEnemy>();
        if (ai != null)
            ai.target = player;

        // Instantiate a copy of the weapon instead of moving the original
        
        GameObject weaponInstance = Instantiate(Weapons[randomWeapon], newZombie.transform);
        Transform holdPoint = newZombie.transform.Find("WeaponHoldPoint");
        if (holdPoint != null)
        {
            Debug.Log("ts aint working");
        }
    }

}
