using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float DetectRadius = 10f;

    Transform Clara;
    NavMeshAgent NavigationAgent;
    Animator EnemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Clara = PlayerManager.instance.player.transform;
        NavigationAgent = GetComponent<NavMeshAgent>();
        EnemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (Clara.position - transform.position).magnitude;

        if (distance <= DetectRadius)
        {
            NavigationAgent.SetDestination(Clara.position);
            EnemyAnimator.SetBool("alert", true);
            if (distance <= NavigationAgent.stoppingDistance)
            {
                // Start Attack
                EnemyAnimator.SetBool("inattacking", true);
                RotateToPlayer();
                EnemyAnimator.SetBool("idle", false);
                EnemyAnimator.SetTrigger("attack");
            }
            else
            {
                EnemyAnimator.SetBool("inattacking", false);
            }
        }
        else
        {
            EnemyAnimator.SetBool("alert", false);
            EnemyAnimator.SetBool("idle", true);
        }
    }

    void RotateToPlayer()
    {
        Vector3 direction = (Clara.position - transform.position).normalized;
        Quaternion ToRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, ToRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectRadius);
    }
}
