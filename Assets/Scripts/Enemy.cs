using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_health = 50;
    public float current_health = 50;
    public float hit_damage = 10;

    GameObject Clara;

    void Start()
    {
        Clara = GameObject.Find("Clara");
    }

    public void Attack()
    {
        Clara.GetComponent<Clara>().ChangeHealthByDiff(-10.0f);
    }

    private void Update()
    {
        if(current_health <= 0.0f)
        {
            Destroy(this);
        }
    }
}
