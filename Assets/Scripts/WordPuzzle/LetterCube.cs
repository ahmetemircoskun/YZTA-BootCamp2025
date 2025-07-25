using UnityEngine;

public class LetterCube : MonoBehaviour
{
    public string letter;
    public Vector2Int gridPos;
    public WordGameManager manager;

    private bool isSelected = false;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        SetColor(Color.gray);
    }

    void OnMouseDown()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            SetColor(Color.yellow);
            manager.AddSelectedLetter(this);
        }
        else
        {
            SetColor(Color.gray);
            manager.RemoveSelectedLetter(this);
        }
    }

    public void SetColor(Color color)
    {
        rend.material.color = color;
    }

    public bool IsSelected() => isSelected;

    public void ResetSelection()
    {
        isSelected = false;
        SetColor(Color.gray);
    }

    public void MarkAsFound()
    {
        SetColor(Color.green);
        enabled = false;
    }
}
