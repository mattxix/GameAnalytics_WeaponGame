using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    public Animator anim;

    // You can remove these if you don’t need them anymore:
    // public Transform eye;
    // public float viewDistance = 10.0f;
    // public float viewAngle = 90.0f;
    // public LayerMask playerMask;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.pathPending || target == null)
            return;

        // Always follow player
        agent.SetDestination(target.position);

        // Attack logic
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        // Moving animation
        anim.SetFloat("Moving", agent.velocity.sqrMagnitude);
    }

}
