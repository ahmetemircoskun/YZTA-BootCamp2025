using UnityEngine;

public class DeadzoneTrigger4_2 : MonoBehaviour
{
    public Vector3 respawnPosition = new Vector3(110.54f, 2.04f, 293.073f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost")) // Buray� "Ghost" yapt�k
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = respawnPosition;
                controller.enabled = true;

                // T�m platformlar� resetle
                PlatformController[] platforms = FindObjectsByType<PlatformController>(FindObjectsSortMode.None);
                foreach (var plat in platforms)
                {
                    plat.ResetPlatform();
                }
            }
        }
    }
}
