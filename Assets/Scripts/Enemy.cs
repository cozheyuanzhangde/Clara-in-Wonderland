using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_health;
    public float current_health;
    private Animator EnemyAnimator;
    GameObject items_spawner;

    //GameObject Clara;

    void Start()
    {
        //Clara = GameObject.Find("Clara");
        EnemyAnimator = GetComponent<Animator>();
        items_spawner = GameObject.Find("ItemsSpawner");
        max_health = 50.0f;
        current_health = max_health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Metal Sword")
        {
            current_health -= 10.0f;
        }
    }

    private void Update()
    {
        if(current_health <= 0.0f)
        {
            items_spawner.GetComponent<SpawnPrefabs>().SpawnThis(new Item { itemName = Item.ItemName.BloodJar, number = 1 });
            Destroy(transform.gameObject);
        }
    }
}
