using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [Header("Wander Settings")]
    public float wanderRadius = 30f;
    private float wanderCooldown = 2f;
    private float wanderTimer;

    public bool shouldChase = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = 0f;
        agent.isStopped = true; // Başlangıçta beklesin
    }

    void Update()
    {
        if (shouldChase)
        {
            if (PlayerHidingDetector.isHiding)
            {
                WanderWhilePlayerHidden(); // Durum 4
            }
            else
            {
                ChasePlayer(); // Durum 3
            }
        }
        else
        {
            agent.isStopped = true; // Durum 2
        }
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void WanderWhilePlayerHidden()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
        {
            Vector3 randomPoint = GetRandomNavMeshPoint(wanderRadius);
            if (randomPoint != Vector3.zero)
            {
                agent.isStopped = false;
                agent.SetDestination(randomPoint);
            }
            wanderTimer = wanderCooldown;
        }
    }

    Vector3 GetRandomNavMeshPoint(float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }

    public void SetChase(bool chase)
    {
        shouldChase = chase;

        if (!chase)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (shouldChase && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Oyun Bitti");
        }
    }
}
