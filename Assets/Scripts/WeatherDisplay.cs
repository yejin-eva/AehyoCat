using System;
using UnityEngine;

public class WeatherDisplay : MonoBehaviour
{
    public bool IsOpen => isOpen;
    public Action OnOpenedWeatherDisplay;
    
    [SerializeField] private TMPro.TextMeshProUGUI weatherDescriptionText;
    [SerializeField] UnityEngine.UI.Image weatherChangeImage;

    [SerializeField] private Sprite defaultWeather;
    [SerializeField] private Sprite thunderstormWeather;
    [SerializeField] private Sprite drizzleWeather;
    [SerializeField] private Sprite rainWeather;
    [SerializeField] private Sprite snowWeather;
    [SerializeField] private Sprite atmosphereWeather;
    [SerializeField] private Sprite clearWeather;
    [SerializeField] private Sprite cloudsWeather;

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
    public void SetWeatherImageByCode(float weatherCode)
    {
        if (weatherCode < 200)
        {
            Debug.LogWarning("no weather display available");
        }
        else if (weatherCode < 300) //thunderstorm
        {
            SetWeatherImage(defaultWeather);
        }
        else if (weatherCode < 500) //drizzle
        {
            SetWeatherImage(defaultWeather);
        }
        else if (weatherCode < 600) // rain 
        {
            SetWeatherImage(defaultWeather);
        }
        else if (weatherCode < 700) // snow
        {
            SetWeatherImage(defaultWeather);
        }
        else if (weatherCode < 800) // atmosphere
        {
            SetWeatherImage(defaultWeather);
        }
        else if (weatherCode == 800) // clear
        {
            SetWeatherImage(defaultWeather);
        }
        else if (weatherCode < 900) // clouds
        {
            SetWeatherImage(defaultWeather);
        }
        else
        {
            Debug.LogWarning("no weather display available");
        }

    }
    private void SetWeatherImage(Sprite sprite)
    {
        weatherChangeImage.sprite = sprite;
    }
    public void SetOpenStatus(bool isOpen)
    {
        this.isOpen = isOpen;
        gameObject.SetActive(isOpen);
    }
}
