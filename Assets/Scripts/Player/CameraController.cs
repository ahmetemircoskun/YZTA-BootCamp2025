using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Hareket Ayarlar�")]
    public float movementSpeed = 5.0f; // Kameran�n hareket h�z�
    public float sprintSpeedMultiplier = 2.0f; 

    [Header("D�n�� Ayarlar� (Fare)")]
    public float rotationSpeed = 2.0f; 
    public float pitchSpeed = 1.0f; 

    [Header("S�n�rlar")]
    public float minPitch = -30.0f; 
    public float maxPitch = 60.0f; 

    private float currentYaw = 0.0f; 
    private float currentPitch = 0.0f; 

    void Start()
    {

    }

    void Update()
    {
        // Klavye ile Hareket 
        float currentMovementSpeed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            currentMovementSpeed *= sprintSpeedMultiplier;
        }

        Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical"); 
        Vector3 sidewaysMovement = transform.right * Input.GetAxis("Horizontal"); 

        Vector3 moveDirection = (forwardMovement + sidewaysMovement).normalized; // Y�n� normalize et
        transform.position += moveDirection * currentMovementSpeed * Time.deltaTime; // Zamana ba�l� hareket

        //  Fare ile D�n�� 
        currentYaw += Input.GetAxis("Mouse X") * rotationSpeed;
        currentPitch -= Input.GetAxis("Mouse Y") * pitchSpeed;

        // Dikey d�n��� s�n�rla
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // Kameran�n yeni rotasyonunu uygula
        transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

    }
}