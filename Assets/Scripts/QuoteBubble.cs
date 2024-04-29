using DG.Tweening;
using UnityEngine;

public class QuoteBubble : MonoBehaviour
{
    [SerializeField] QuotesSO quotes;
    [SerializeField] private TMPro.TextMeshProUGUI quotesText;
    [SerializeField] private GameObject quoteBubble;

    private int lastIndex = -1;
    private Vector3 originalLocalScale;
    private void Awake()
    {
        this.gameObject.SetActive(false);
        this.originalLocalScale = transform.localScale;
    }
    private void OnEnable()
    {
        OpenQuoteBubble();
    }

    private void OpenQuoteBubble()
    {
        UpdateQuoteText();
        transform.DOScale(originalLocalScale, 0.3f).From(originalLocalScale * 0.5f).SetEase(Ease.OutBack);
    }
    public void SetQuoteText(string message)
    {
        UpdateQuoteText(message);
    }
    private void UpdateQuoteText(string message = "")
    {
        if (message != "")
        {
            quotesText.text = message;
        }
        else
        {
            int index;
            do
            {
                index = UnityEngine.Random.Range(0, quotes.quotes.Length);
            }
            while (index == lastIndex && quotes.quotes.Length > 1);
            quotesText.text = quotes.quotes[index];

            lastIndex = index;
        }
        Invoke(nameof(HideQuoteBubble), 2f);
    }

    private void HideQuoteBubble()
    {
        transform.DOScale(originalLocalScale * 0.5f, 0.3f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
        quoteBubble.SetActive(true);
    }
}
