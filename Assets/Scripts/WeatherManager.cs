using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public bool IsOpen => isOpen;

    [SerializeField] private WeatherAPIManager weatherAPIManager;
    [SerializeField] private TMPro.TextMeshProUGUI weatherDescriptionText;

    private bool isOpen;
    private void Awake()
    {
        SetOpenStatus(isOpen);
    }
    private void OnEnable()
    {
        UpdateWeatherDescription();
        UpdateWeatherConditionCode();

    }
    private void UpdateWeatherDescription()
    {
        string weatherDescription = weatherAPIManager.GetWeatherDescription();
        weatherDescriptionText.text = $"Current Weather: {weatherDescription}";
    }
    private void UpdateWeatherConditionCode()
    {
        float weatherConditionCode = weatherAPIManager.GetWeatherConditionCode();
        Debug.Log(weatherConditionCode);
    }

    public void SetOpenStatus(bool isOpen)
    {
        this.isOpen = isOpen;
        gameObject.SetActive(isOpen);
    }


}
