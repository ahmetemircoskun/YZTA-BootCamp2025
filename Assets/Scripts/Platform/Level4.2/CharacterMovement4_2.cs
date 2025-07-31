using UnityEngine;

public class CharacterMovement4_2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float ySpeed;
    [SerializeField] private float originalStepOffset;

    private Vector3 respawnPosition4_2 = new Vector3(110.54f, 2.04f, 293.073f); // <-- Deadzone ışınlama pozisyonu

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
        transform.position = respawnPosition4_2;
        characterController.enabled = true;

        // Platformları resetle
        PlatformController4_2[] platforms = FindObjectsByType<PlatformController4_2>(FindObjectsSortMode.None);
        foreach (var plat in platforms)
        {
            plat.ResetPlatform();
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        PlatformController4_2 platform = hit.collider.GetComponent<PlatformController4_2>();
        if (platform != null)
        {
            platform.TriggerFall();
            platform.SetTemporarySafe(true);
        }
    }
}
