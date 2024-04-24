using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherAPIManager : MonoBehaviour
{
    
    public string GetWeatherDescription()
    {
        WeatherData weatherData = WeatherAPIHelper.GetWeatherData();

        return weatherData.weather[0].description;
    }

    public float GetWeatherConditionCode()
    {
        WeatherData weatherData = WeatherAPIHelper.GetWeatherData();

        return weatherData.weather[0].id;
    }
}
