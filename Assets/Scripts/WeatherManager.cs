using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public bool IsOpen => isOpen;

    [SerializeField] private WeatherAPIManager weatherAPIManager;
    [SerializeField] private TMPro.TextMeshProUGUI weatherDescriptionText;
    [SerializeField] private float timeoutSeconds = 3f;

    private bool isOpen;
    private void Awake()
    {
        SetOpenStatus(isOpen);
    }
    private void OnEnable()
    {
        StartCoroutine(UpdateWeatherDescriptionWithTimeout());

    }
    private IEnumerator UpdateWeatherDescriptionWithTimeout()
    {
        bool completed = false;
        StartCoroutine(UpdateWeatherInformation(() => completed = true));

        float startTime = Time.time;
        while (!completed && Time.time - startTime < timeoutSeconds)
        {
            yield return null;
        }
        if (!completed)
        {
            weatherDescriptionText.text = "Failed to get weather data";
            Debug.Log("Failed to fetch weather description in time, using default.");
        }
    
    }
    private IEnumerator UpdateWeatherInformation(System.Action onComplete)
    {
        WeatherData weatherData = WeatherAPIHelper.GetWeatherData();
        string weatherDescription = weatherData.weather[0].description;
        weatherDescriptionText.text = $"Current Weather: {weatherDescription}";
        onComplete?.Invoke();
        yield return null;
    }
    public void SetOpenStatus(bool isOpen)
    {
        this.isOpen = isOpen;
        gameObject.SetActive(isOpen);
    }


}
