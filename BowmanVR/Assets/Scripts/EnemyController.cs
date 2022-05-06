using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    public GameObject blood;
    public Transform target;
    NavMeshAgent agent;
    
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {

            agent.SetDestination(target.position);

            animator.SetFloat("Velocity", agent.velocity.magnitude);

            if (distance >= agent.stoppingDistance)
            {
                FaceTarget();
            }
            else if(distance <= agent.stoppingDistance)
            {
                animator.SetBool("AtEnemy", true);                
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("projectile"))
        {
            Player.score++;
            animator.SetBool("Hit", true);
            agent.isStopped = true;
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
            gameObject.GetComponentInChildren<CapsuleCollider>().center = new Vector3(10f, gameObject.GetComponentInChildren<CapsuleCollider>().center.y, 10f);
            StartCoroutine("Die");

        }
       
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            Player.headShots++;
            animator.SetBool("Hit", true);
            agent.isStopped = true;
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
            gameObject.GetComponentInChildren<CapsuleCollider>().center = new Vector3(10f, gameObject.GetComponentInChildren<CapsuleCollider>().center.y, 10f);
            StartCoroutine("Die");

        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}

