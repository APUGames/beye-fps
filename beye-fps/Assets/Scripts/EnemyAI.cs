using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Target is the player
    [SerializeField] Transform target;
    //range from player to enemy before chase
    [SerializeField] float chaseRange = 5.0f;
    
    [SerializeField] float turnSpeed = 5.0f;

    NavMeshAgent nMA;

    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        nMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // measure distance between enemy and player
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }

        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }

    }

    private void EngageTarget()
    {
        FaceTarget();

        if(distanceToTarget >= nMA.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= nMA.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("run");
        nMA.SetDestination(target.position);
    }
    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        //print(name + " is attacking " + target.name);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    private void OnDrawGizmosSelected()
    {
        //display chase range
        Gizmos.color = new Color(1,0,0,1.0f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
