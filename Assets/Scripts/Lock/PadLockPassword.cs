using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    private MoveRuller _moveRull;

    public int[] _numberPassword = { 0, 0, 0, 0 };

    private void Awake()
    {
        _moveRull = FindFirstObjectByType<MoveRuller>();
    }

    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            Debug.Log("Password correct");

            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                var emission = _moveRull._rullers[i].GetComponent<PadLockEmissionColor>();
                emission._isSelect = false;
                emission.BlinkingMaterial();
            }
        }
    }
}
