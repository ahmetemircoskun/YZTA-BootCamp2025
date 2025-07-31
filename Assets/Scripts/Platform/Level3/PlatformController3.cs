using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool isSafe = false;

    private bool triggered = false;
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

        // Baþlangýçta renk basic (beyaz)
        UpdateColor();
    }

    public void TriggerFall()
    {
        if (!isSafe && !triggered)
        {
            triggered = true;
            rb.isKinematic = false;
        }
    }

    public void ResetPlatform()
    {
        if (rb.isKinematic == false)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        rb.isKinematic = true;

        transform.position = startPosition;
        transform.rotation = startRotation;

        triggered = false;

        // Renk resetlenmesin, önceki yeþil kalsýn
        UpdateColor();
    }

    public void SetTemporarySafe(bool value)
    {
        isTemporarilySafe = value;
        UpdateColor();
    }

    private void UpdateColor()
    {
        Renderer rend = GetComponent<Renderer>();

        if (isTemporarilySafe)
            rend.material.color = Color.green;
        else
            rend.material.color = new Color32(0x6F, 0x77, 0x82, 0xFF); // #6F7782 gri-mavi
    }
}
