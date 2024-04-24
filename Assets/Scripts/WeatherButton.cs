using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherButton : MonoBehaviour
{
    [SerializeField] private WeatherManager weatherManager;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnWeatherButtonClicked());
    }
    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }
    private void OnWeatherButtonClicked()
    {
        weatherManager.SetOpenStatus(!weatherManager.IsOpen);
    }
}
