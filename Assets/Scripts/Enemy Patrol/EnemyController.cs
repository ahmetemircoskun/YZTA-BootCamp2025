using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [Header("Takip Ayarları")]
    public bool shouldChase = false;

    [Header("Saklanma Durumunda Gezinme")]
    public float wanderRadius = 30f;
    public float wanderCooldown = 2f;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        wanderTimer = 0f;
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

    // ✅ Yakalama burada olur
    private void OnTriggerEnter(Collider other)
    {
        if (shouldChase && other.CompareTag("Player"))
        {
            Debug.Log("Oyun Bitti! Oyuncuya değildi.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
