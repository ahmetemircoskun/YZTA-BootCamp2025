using System.Collections.Generic;
using UnityEngine;

public class MoveRuller2 : MonoBehaviour
{
    PadLockPassword2 _lockPassword2;
    PadLockEmissionColor _pLockColor;

    [HideInInspector]
    public List<GameObject> _rullers = new List<GameObject>();

    private int _scroolRuller = 0;
    private int _changeRuller = 0;

    [HideInInspector]
    public int[] _numberArray = { 0, 0, 0, 0 };

    private int _numberRuller = 0;
    private bool _isActveEmission = false;

    void Awake()
    {
        _lockPassword2 = FindFirstObjectByType<PadLockPassword2>();
        _pLockColor = FindFirstObjectByType<PadLockEmissionColor>();

        _rullers.Add(GameObject.Find("Ruller1"));
        _rullers.Add(GameObject.Find("Ruller2"));
        _rullers.Add(GameObject.Find("Ruller3"));
        _rullers.Add(GameObject.Find("Ruller4"));

        foreach (GameObject r in _rullers)
        {
            r.transform.Rotate(-144, 0, 0, Space.Self);
        }
    }

    void Update()
    {
        MoveRulles();
        RotateRullers();

        // Sadece PadLockPassword2 varsa onu çalıştır
        if (_lockPassword2 != null)
            _lockPassword2.Password();
    }

    void MoveRulles()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _isActveEmission = true;
            _changeRuller++;
            _numberRuller++;

            if (_numberRuller > 3)
                _numberRuller = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _isActveEmission = true;
            _changeRuller--;
            _numberRuller--;

            if (_numberRuller < 0)
                _numberRuller = 3;
        }

        _changeRuller = (_changeRuller + _rullers.Count) % _rullers.Count;

        for (int i = 0; i < _rullers.Count; i++)
        {
            var emission = _rullers[i].GetComponent<PadLockEmissionColor>();
            emission._isSelect = (_isActveEmission && _changeRuller == i);
            emission.BlinkingMaterial();
        }
    }

    void RotateRullers()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _isActveEmission = true;
            _scroolRuller = 36;
            _rullers[_changeRuller].transform.Rotate(-_scroolRuller, 0, 0, Space.Self);

            _numberArray[_changeRuller]++;
            if (_numberArray[_changeRuller] > 9)
                _numberArray[_changeRuller] = 0;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _isActveEmission = true;
            _scroolRuller = 36;
            _rullers[_changeRuller].transform.Rotate(_scroolRuller, 0, 0, Space.Self);

            _numberArray[_changeRuller]--;
            if (_numberArray[_changeRuller] < 0)
                _numberArray[_changeRuller] = 9;
        }
    }
}
