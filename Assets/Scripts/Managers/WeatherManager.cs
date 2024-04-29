using System.Threading.Tasks;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private WeatherDisplay weatherDisplay;
    [SerializeField] private float updateInterval = 60f;

    private float timer = Mathf.Infinity;
    private float previousWeatherCode;
    private string previousWeatherDescription;
    
    private void OnEnable()
    {
        weatherDisplay.OnOpenedWeatherDisplay += OnWeatherDisplayOpened;
        WeatherAPIHelper.OnTimeOut += OnWeatherInformationRequestTimeOut;
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
            
            _ = UpdateWeatherInformation(() => { });
        }
        else
        {
            weatherDisplay.SetWeatherDescriptionText($"Current Weather: {previousWeatherDescription}");
            weatherDisplay.SetWeatherImageByCode(previousWeatherCode);
        }
        
    }

    private async Task UpdateWeatherInformation(System.Action onComplete)
    {
        weatherDisplay.SetWeatherDescriptionText("Loading weather...");

        WeatherData weatherData = await WeatherAPIHelper.GetWeatherData();
        string weatherDescription = weatherData.weather[0].description;
        float weatherCode = weatherData.weather[0].id;

        weatherDisplay.SetWeatherDescriptionText($"Current Weather: {weatherDescription}");
        weatherDisplay.SetWeatherImageByCode(weatherCode);

        previousWeatherCode = weatherCode;
        previousWeatherDescription = weatherDescription;

        onComplete?.Invoke();
    }

    private void OnWeatherInformationRequestTimeOut()
    {
        weatherDisplay.SetWeatherDescriptionText("Failed to get weather data");
        Debug.LogWarning("Failed to fetch weather description in time, using default.");
    }
}
