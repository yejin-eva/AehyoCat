using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetWeatherDescription();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetWeatherConditionCode();
        }
    }
    public string GetWeatherDescription()
    {
        WeatherData weatherData = WeatherAPIHelper.GetWeatherData();
        Debug.Log(weatherData.weather[0].description);

        return weatherData.weather[0].description;
    }

    public float GetWeatherConditionCode()
    {
        WeatherData weatherData = WeatherAPIHelper.GetWeatherData();
        Debug.Log(weatherData.weather[0].id);

        return weatherData.weather[0].id;
    }
}
