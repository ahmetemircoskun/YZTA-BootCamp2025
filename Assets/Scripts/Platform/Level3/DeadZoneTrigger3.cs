using UnityEngine;

public class DeadzoneTrigger3 : MonoBehaviour
{
    public Vector3 respawnPosition = new Vector3(90f, 1.71f, 65f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Artık Player tag'i
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = respawnPosition;
                controller.enabled = true;

                // Tüm platformları resetle
                PlatformController[] platforms = FindObjectsByType<PlatformController>(FindObjectsSortMode.None);
                foreach (var plat in platforms)
                {
                    plat.ResetPlatform();
                }
            }
        }
    }
}
