using UnityEngine;

public class WeatherButton : MonoBehaviour
{
    [SerializeField] private WeatherDisplay weatherDisplay;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnWeatherButtonClicked());
    }
    private void OnDestroy()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }
    private void OnWeatherButtonClicked()
    {
        weatherDisplay.SetOpenStatus(!weatherDisplay.IsOpen);
    }
}
