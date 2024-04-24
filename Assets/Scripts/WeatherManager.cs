using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private WeatherDisplay weatherDisplay;
    [SerializeField] private float timeoutSeconds = 3f;

    
    private void OnEnable()
    {
        weatherDisplay.OnOpenedWeatherDisplay += OnWeatherDisplayOpened;
    }

    private void OnWeatherDisplayOpened()
    {
        StartCoroutine(UpdateWeatherDescriptionWithTimeout());
    }

    private IEnumerator UpdateWeatherDescriptionWithTimeout()
    {
        weatherDisplay.SetWeatherDescriptionText("Loading weather data...");

        bool completed = false;
        StartCoroutine(UpdateWeatherInformation(() => completed = true));

        float startTime = Time.time;
        while (!completed && Time.time - startTime < timeoutSeconds)
        {
            yield return null;
        }
        if (!completed)
        {
            weatherDisplay.SetWeatherDescriptionText("Failed to get weather data");
            Debug.Log("Failed to fetch weather description in time, using default.");
        }
    
    }
    private IEnumerator UpdateWeatherInformation(System.Action onComplete)
    {
        WeatherData weatherData = WeatherAPIHelper.GetWeatherData();
        string weatherDescription = weatherData.weather[0].description;
        weatherDisplay.SetWeatherDescriptionText($"Current Weather: {weatherDescription}");
        onComplete?.Invoke();
        yield return null;
    }
    


}
