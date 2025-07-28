using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public EnemyController enemyAI;

    private bool toggle = false;
    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            toggle = !toggle;
            enemyAI.SetChase(toggle);
            Debug.Log("Player tetikleyiciye girdi. shouldChase = " + toggle);
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
