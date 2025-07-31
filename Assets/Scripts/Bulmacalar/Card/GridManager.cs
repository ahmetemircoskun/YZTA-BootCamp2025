using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GridManager : MonoBehaviour
{
    public static GridManager Instance; // Singleton referansı
    public Transform cardParent; // tüm kartların atanacağı ebeveyn
    public int rows = 3;
    public int columns = 3;
    public GameObject cardAPrefab;
    public GameObject cardBPrefab;
    public float spacing = 1.2f;
    private bool isProcessing = false;

    private List<Card> selectedCards = new List<Card>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateBoard();
        PositionCamera();
    }

    void GenerateBoard()
    {
        int totalCards = rows * columns;
        int bCount = 2;
        int aCount = totalCards - bCount;

        List<GameObject> cards = new List<GameObject>();
        for (int i = 0; i < aCount; i++) cards.Add(cardAPrefab);
        for (int i = 0; i < bCount; i++) cards.Add(cardBPrefab);

        // Karıştır
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject temp = cards[i];
            int rand = Random.Range(i, cards.Count);
            cards[i] = cards[rand];
            cards[rand] = temp;
        }

        // Grid'e yerleştir
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                Vector3 position = new Vector3(col * spacing, 0.25f, row * spacing);
                GameObject cardObj = Instantiate(cards[index], position, Quaternion.identity, cardParent);
            }
        }
    }

    void PositionCamera()
    {
        Vector3 center = new Vector3((columns - 1) * spacing / 2f, 10, (rows - 1) * spacing / 2f);
        Camera.main.transform.position = center;
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    public void CardSelected(Card card)
    {
        if (selectedCards.Contains(card)) return;
        if (selectedCards.Count >= 2 || isProcessing) return;

        selectedCards.Add(card);

        if (selectedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isProcessing = true;

        yield return new WaitForSeconds(1f);

        if (selectedCards[0].GetCardType() == Card.CardType.B &&
            selectedCards[1].GetCardType() == Card.CardType.B)
        {
            Debug.Log("Kazandın!");
            CardSoundsManager.Instance.PlayMatchSound();    // DOĞRU SES
            Card.CheckSolutionStatic();                    // DOĞRU FONKSİYON
        }
        else
        {
            Debug.Log("Yanlış eşleşme.");
            CardSoundsManager.Instance.PlayMismatchSound(); // YANLIŞ SES
            selectedCards[0].Hide();
            selectedCards[1].Hide();
        }

        selectedCards.Clear();
        isProcessing = false;
    }
}
