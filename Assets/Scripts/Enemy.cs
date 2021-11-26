using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_health;
    public float current_health;
    private Animator EnemyAnimator;

    //GameObject Clara;

    void Start()
    {
        //Clara = GameObject.Find("Clara");
        EnemyAnimator = GetComponent<Animator>();
        max_health = 50.0f;
        current_health = max_health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Metal Sword")
        {
            current_health -= 15.0f;
        }
    }

    private void Update()
    {
        if(current_health <= 0.0f)
        {
            Destroy(transform.gameObject);
        }
    }
}
