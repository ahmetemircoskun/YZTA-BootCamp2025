using UnityEngine;

public class InvertedMovementController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Bileşenler")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    private float verticalVelocity;
    private float defaultStepOffset;
    private bool isMovementInverted = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        defaultStepOffset = characterController.stepOffset;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // 1. Input al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (isMovementInverted)
        {
            horizontal *= -1f;
            vertical *= -1f;
        }

        // 2. Kameraya göre yön belirle
        Transform cam = Camera.main.transform;
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * vertical + camRight * horizontal;

        // 3. Animasyon parametresi
        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;
        animator.SetFloat("Speed", inputMagnitude);

        // 4. Hareket
        float speed = Mathf.Clamp01(moveDir.magnitude) * moveSpeed;
        moveDir.Normalize();

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = defaultStepOffset;
            verticalVelocity = -0.5f;

            // İleride zıplama animasyonu burada kontrol edilebilir
            // if (Input.GetButtonDown("Jump")) {...}
        }
        else
        {
            characterController.stepOffset = 0f;
        }

        Vector3 velocity = moveDir * speed;
        velocity.y = verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InvertTrigger"))
        {
            isMovementInverted = !isMovementInverted;
            Debug.Log("Hareket yönü tersine çevrildi: " + isMovementInverted);
        }
    }
}
