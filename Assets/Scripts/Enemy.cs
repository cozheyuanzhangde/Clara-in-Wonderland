using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_health;
    public float current_health;
    GameObject items_spawner;

    GameObject Clara;
    AudioSource SlimeAudio;
    public AudioClip SlimeGetHit;

    void Start()
    {
        Clara = GameObject.Find("Clara");
        items_spawner = GameObject.Find("ItemsSpawner");
        max_health = 50.0f;
        current_health = max_health;
        gameObject.AddComponent<AudioSource>();
        SlimeAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Metal Sword")
        {
            bool attackA = Clara.GetComponent<Invector.vCharacterController.vMeleeCombatInput>().animator.GetCurrentAnimatorStateInfo(Clara.GetComponent<Invector.vCharacterController.vMeleeCombatInput>().animator.GetLayerIndex("FullBody")).IsName("A");
            bool attackB = Clara.GetComponent<Invector.vCharacterController.vMeleeCombatInput>().animator.GetCurrentAnimatorStateInfo(Clara.GetComponent<Invector.vCharacterController.vMeleeCombatInput>().animator.GetLayerIndex("FullBody")).IsName("B");
            bool attackC = Clara.GetComponent<Invector.vCharacterController.vMeleeCombatInput>().animator.GetCurrentAnimatorStateInfo(Clara.GetComponent<Invector.vCharacterController.vMeleeCombatInput>().animator.GetLayerIndex("FullBody")).IsName("C");
            if (attackA || attackB || attackC)
            {
                SlimeAudio.PlayOneShot(SlimeGetHit, 1.0f);
                current_health -= 10.0f;
            }
            
        }
    }

    private void Update()
    {
        if (current_health <= 0.0f)
        {
            //items_spawner.GetComponent<SpawnPrefabs>().SpawnThis(new Item { itemName = Item.ItemName.BloodJar, number = 1 });
            items_spawner.GetComponent<SpawnPrefabs>().SpawnThis(new Item { itemName = Item.ItemName.SlimeMeat, number = 1 });
            Destroy(transform.gameObject);
        }
    }
}
