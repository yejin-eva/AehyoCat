using UnityEngine;

public class WeatherButton : MonoBehaviour
{
    [SerializeField] private WeatherDisplay weatherDisplay;

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
        weatherDisplay.SetOpenStatus(!weatherDisplay.IsOpen);
    }
}
