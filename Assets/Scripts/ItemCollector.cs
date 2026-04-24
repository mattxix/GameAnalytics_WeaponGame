using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class ItemCollector : MonoBehaviour
{
    public int itemsCollected, itemsInLevel;
    public TMP_Text itemHud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemHud.text = $"Coins {itemsCollected}/{itemsInLevel}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemCollect()
    {
        itemsCollected++; 
        itemHud.text = $"Coins {itemsCollected}/{itemsInLevel}";

        if(itemsCollected >= itemsInLevel)
        {
            //Change scene or end game...
            StartCoroutine(GameOver());
        }

        IEnumerator GameOver()
        {
            itemHud.text = $"You collected all {itemsInLevel} items!";
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(1);
        }

   
    }
}
