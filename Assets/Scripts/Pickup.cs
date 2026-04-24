using UnityEngine;

public class Pickup : MonoBehaviour
{
    ItemCollector collector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collector = GameObject.Find("CoinHUD").GetComponent<ItemCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collector.ItemCollect();
            Destroy(gameObject);
        }
    }
}
