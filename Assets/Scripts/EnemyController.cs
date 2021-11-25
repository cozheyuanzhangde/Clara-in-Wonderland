using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float DetectRadius = 10f;

    Transform Clara;
    NavMeshAgent NavigationAgent;

    // Start is called before the first frame update
    void Start()
    {
        Clara = PlayerManager.instance.player.transform;
        NavigationAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (Clara.position - transform.position).magnitude;

        if(distance <= DetectRadius)
        {
            NavigationAgent.SetDestination(Clara.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectRadius);
    }
}
