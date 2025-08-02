using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float ySpeed;
    [SerializeField] private float originalStepOffset;

    private Vector3 respawnPosition3 = new Vector3(90f, 1.71f, 66.5f); // <-- Deadzone ışınlama pozisyonu
    private Vector3 respawnPosition42 = new Vector3(110f, 1.71f, 300); // <-- Deadzone ışınlama pozisyonu
    private float deadZoneY = -10f; // <-- Bu değeri sahnene göre ayarlayabilirsin

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection = forward * verticalInput + right * horizontalInput;
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Zıplama tetiklendi");
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        // DEADZONE kontrolü
        if (transform.position.y < deadZoneY)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        characterController.enabled = false;
        transform.position = respawnPosition3;
        characterController.enabled = true;

        // Platformları resetle
        PlatformController[] platforms = FindObjectsByType<PlatformController>(FindObjectsSortMode.None);
        foreach (var plat in platforms)
        {
            plat.ResetPlatform();
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        PlatformController platform = hit.collider.GetComponent<PlatformController>();
        if (platform != null)
        {
            platform.TriggerFall();

            if (platform.isSafe)
            {
                platform.TriggerPlatform();
                platform.SetTemporarySafe(true);
            }
            else
            {
                platform.SetTemporarySafe(false);
            }
        }
    }

}
