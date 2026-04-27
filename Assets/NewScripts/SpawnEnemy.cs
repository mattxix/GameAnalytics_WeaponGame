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

        Weapons[randomWeapon].transform.SetParent(newZombie.transform);
        Weapons[randomWeapon].transform.localPosition = Vector3.zero;
        Weapons[randomWeapon].transform.localRotation = Quaternion.identity;
       
    }

}
