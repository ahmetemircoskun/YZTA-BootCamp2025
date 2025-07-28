using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Referanslar")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Takip Ayarları")]
    public bool shouldChase = false;
    public float stopDistance = 0.5f; // Yan yana sayılacak mesafe

    [Header("Saklanma Durumunda Dolaşma")]
    public float wanderRadius = 30f;
    public float wanderCooldown = 2f;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = 0f;
        agent.isStopped = true;
    }

    void Update()
    {
        if (!shouldChase)
        {
            agent.isStopped = true;
            return;
        }

        if (PlayerHidingDetector.isHiding)
        {
            WanderWhilePlayerHidden();
        }
        else
        {
            ChasePlayer();
            CheckPlayerDistance();
        }
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= stopDistance)
        {
            Debug.Log("Oyun Bitti - Düşman oyuncuya ulaştı");
            // Buraya oyun bitirme işlemi eklenebilir (sahne geçişi, panel açma vs.)
        }
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

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 5f, NavMesh.AllAreas))
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
}
