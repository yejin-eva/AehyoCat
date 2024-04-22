using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteBubble : MonoBehaviour
{
    [SerializeField] Quotes quotes;
    [SerializeField] private TMPro.TextMeshProUGUI quotesText;

    private int lastIndex = -1;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        UpdateQuoteText();
    }
    
    private void UpdateQuoteText()
    {
        int index;
        do
        {
            index = UnityEngine.Random.Range(0, quotes.quotes.Length);
        }
        while (index == lastIndex && quotes.quotes.Length > 1);
        quotesText.text = quotes.quotes[index];

        lastIndex = index;

        Invoke(nameof(HideQuoteBubble), 2f);
    }

    private void HideQuoteBubble()
    {
        this.gameObject.SetActive(false);
    }
}
