using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;           // Döndürülecek kapı (örn: pivotu menteşeye ayarla)
    public float openAngle = -100f;    // Kapı açılınca döneceği açı
    public float openSpeed = 3f;     // Açılma hızı
    public KeyCode interactKey = KeyCode.E;

    private bool playerInTrigger = false;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (door == null)
            door = transform;

        closedRotation = door.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
        }

        // Kapıyı akıcı döndür
        if (isOpen)
            door.rotation = Quaternion.Lerp(door.rotation, openRotation, Time.deltaTime * openSpeed);
        else
            door.rotation = Quaternion.Lerp(door.rotation, closedRotation, Time.deltaTime * openSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInTrigger = false;
    }
}
