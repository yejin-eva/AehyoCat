using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private WeatherDisplay weatherDisplay;
    [SerializeField] private float timeoutSeconds = 3f;
    //[SerializeField] private float updateInterval = 60f;

    private float timer = 0f;

    
    private void OnEnable()
    {
        weatherDisplay.OnOpenedWeatherDisplay += OnWeatherDisplayOpened;
    }
    //private void Update()
    //{
    //    UpdateTimer();
    //    if (timer >= updateInterval)
    //    {
    //        timer = 0f;
    //        StartCoroutine(UpdateWeatherDescriptionWithTimeout());
    //    }
    //}

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
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
        float weatherCode = weatherData.weather[0].id;

        weatherDisplay.SetWeatherDescriptionText($"Current Weather: {weatherDescription}");
        weatherDisplay.SetWeatherImageByCode(weatherCode);
        
        onComplete?.Invoke();
        yield return null;
    }
}
