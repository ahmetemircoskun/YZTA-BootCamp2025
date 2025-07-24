using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   [SerializeField] private float speed = 5f;
    //[SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float jumpSpeed= 5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float ySpeed;
    [SerializeField] private float originalStepOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    // Kameraya göre hareket yönü
    Vector3 forward = Camera.main.transform.forward;
    Vector3 right = Camera.main.transform.right;

    // Yatay düzlemde tut
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
    }
}
