using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private CharacterController controller;
    private float pitch = 0f;
    private Vector3 startPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        startPosition = transform.position;
    }

    void Update()
    {
        Move();
        LookAround();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.SimpleMove(move * moveSpeed);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        PlatformController platform = hit.collider.GetComponent<PlatformController>();
        if (platform != null)
        {
            Vector3 hitNormal = hit.normal;
            float dot = Vector3.Dot(hitNormal, Vector3.up);

            if (dot > 0.5f)
            {
                platform.SetTemporarySafe(true);

                if (!platform.isSafe)
                {
                    platform.TriggerFall();
                }
            }
        }

        if (hit.collider.CompareTag("Deadzone"))
        {
            controller.enabled = false;
            transform.position = startPosition;
            controller.enabled = true;

            PlatformController[] allPlatforms = Object.FindObjectsByType<PlatformController>(FindObjectsSortMode.None);
            foreach (PlatformController p in allPlatforms)
            {
                p.ResetPlatform();
            }
        }
    }
}
