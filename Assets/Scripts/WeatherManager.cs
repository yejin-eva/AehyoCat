using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private WeatherDisplay weatherDisplay;
    [SerializeField] private float timeoutSeconds = 3f;
    [SerializeField] private float updateInterval = 60f;

    private float timer = Mathf.Infinity;
    private float previousWeatherCode;
    private string previousWeatherDescription;
    
    private void OnEnable()
    {
        weatherDisplay.OnOpenedWeatherDisplay += OnWeatherDisplayOpened;
    }
    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    private void OnWeatherDisplayOpened()
    {
        if (timer >= updateInterval)
        {
            timer = 0f;
            StartCoroutine(UpdateWeatherDescriptionWithTimeout());
        }
        else
        {
            weatherDisplay.SetWeatherDescriptionText($"Current Weather: {previousWeatherDescription}");
            weatherDisplay.SetWeatherImageByCode(previousWeatherCode);
        }
        
    }

    private IEnumerator UpdateWeatherDescriptionWithTimeout()
    {
        weatherDisplay.SetWeatherDescriptionText("Loading weather data...");

        bool completed = false;

        _ = UpdateWeatherInformation(() => completed = true);

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
    private async Task UpdateWeatherInformation(System.Action onComplete)
    {
        WeatherData weatherData = await WeatherAPIHelper.GetWeatherData();
        string weatherDescription = weatherData.weather[0].description;
        float weatherCode = weatherData.weather[0].id;

        weatherDisplay.SetWeatherDescriptionText($"Current Weather: {weatherDescription}");
        weatherDisplay.SetWeatherImageByCode(weatherCode);

        previousWeatherCode = weatherCode;
        previousWeatherDescription = weatherDescription;

        onComplete?.Invoke();
    }
}
