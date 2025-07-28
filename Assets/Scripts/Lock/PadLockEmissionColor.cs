using UnityEngine;

public class PadLockEmissionColor : MonoBehaviour
{
    TimeBlinking tb;

    private GameObject _myRuller;

    [HideInInspector]
    public bool _isSelect;

    private void Awake()
    {
        tb = FindFirstObjectByType<TimeBlinking>();
    }

    void Start()
    {
        _myRuller = gameObject;
    }

    public void BlinkingMaterial()
    {
        _myRuller.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        if (_isSelect)
        {
            Color dimYellow = new Color(0.5f, 0.3f, 0f);
            _myRuller.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.clear, dimYellow, Mathf.PingPong(Time.time, tb.blinkingTime)));
        }
        else
        {
            _myRuller.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
        }
    }
}