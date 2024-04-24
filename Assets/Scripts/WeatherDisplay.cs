using System;
using UnityEngine;

public class WeatherDisplay : MonoBehaviour
{
    public bool IsOpen => isOpen;
    public Action OnOpenedWeatherDisplay;
    
    [SerializeField] private TMPro.TextMeshProUGUI weatherDescriptionText;

    private bool isOpen;
    private void Awake()
    {
        SetOpenStatus(isOpen);
    }
    private void OnEnable()
    {
        OnOpenedWeatherDisplay?.Invoke();
    }
    public void SetWeatherDescriptionText(string text)
    {
        weatherDescriptionText.text = text;
    }
    // Start is called before the first frame update
    public void SetOpenStatus(bool isOpen)
    {
        this.isOpen = isOpen;
        gameObject.SetActive(isOpen);
    }
}
