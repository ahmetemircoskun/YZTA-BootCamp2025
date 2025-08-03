using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool isSafe = false;
    private bool isActivated = false;
    private bool isTemporarilySafe = false;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb.isKinematic = true;
        UpdateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isSafe)
            {
                TriggerPlatform();
            }
            else
            {
                TriggerFall();
            }
        }
    }

    public void TriggerPlatform()
    {
        if (!isActivated)
        {
            isActivated = true;
            UpdateColor();
        }
    }

    public void TriggerFall()
    {
        if (!isSafe && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
    }

    public void ResetPlatform()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        rb.isKinematic = true;
        transform.position = startPosition;
        transform.rotation = startRotation;
        UpdateColor();
    }

    // SetTemporarySafe karakter kontrolünde bir yerde çağrılıyorsa kullan
    public void SetTemporarySafe(bool value)
    {
        isTemporarilySafe = value;
        UpdateColor();
    }

    private void UpdateColor()
    {
        Renderer rend = GetComponent<Renderer>();
        if (isTemporarilySafe)
            rend.material.color = new Color(0.3f, 0.6f, 0.3f); // Soft yeşil (geçici)
        else if (isSafe && isActivated)
            rend.material.color = new Color(0.3f, 0.6f, 0.3f); // Soft yeşil (kalıcı)
        else
            rend.material.color = new Color32(0x6F, 0x77, 0x82, 0xFF); // gri-mavi
    }
}
