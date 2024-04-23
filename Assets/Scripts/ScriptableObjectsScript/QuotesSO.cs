using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quotes", menuName = "Quotes")]
public class QuotesSO : ScriptableObject
{
    public string[] quotes;
    public string GetRandomQuote()
    {
        return quotes[Random.Range(0, quotes.Length)];
    }
}
