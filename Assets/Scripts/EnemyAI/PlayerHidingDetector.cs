using UnityEngine;

public class PlayerHidingDetector : MonoBehaviour
{
     public static bool isHiding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHiding = true;
            Debug.Log("Player saklanıyor");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHiding = false;
            Debug.Log("Player saklandığı yerden çıktı");
        }
    }
}
