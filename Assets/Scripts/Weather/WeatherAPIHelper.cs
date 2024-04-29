using System.Net;
using UnityEngine;
using Cysharp.Threading.Tasks; 
using System.Threading;
using System;
using UnityEngine.Networking;

public static class WeatherAPIHelper
{
    public static Action OnTimeOut;
    static int cancelTime = 3000; // Milliseconds

    static float latitude = 37.568291f;
    static float longitude = 126.997780f;

    public static float Latitude
    {
        get => latitude;
        set => latitude = value;
    }
    public static float Longitude
    {
        get => longitude;
        set => longitude = value;
    }

    public static async UniTask<WeatherData> GetWeatherData()
    {
        using (CancellationTokenSource cts = new CancellationTokenSource(cancelTime))
        {
            var request = UnityWebRequest.Get($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={ApiKeys.weatherApiKey}");
            try
            {
                // UnityWebRequest 방식
                await request.SendWebRequest().WithCancellation(cts.Token);

                // 요청이 성공적이면
                if (request.result == UnityWebRequest.Result.Success)
                {
                    return JsonUtility.FromJson<WeatherData>(request.downloadHandler.text);
                }
                // 요청이 실패하면
                else
                {
                    Debug.LogError($"Error: {request.error}");
                }
            }
            catch (OperationCanceledException)
            {
                Debug.LogError("The request was canceled due to a timeout.");
                OnTimeOut?.Invoke();
            }
            catch (WebException ex)
            {
                Debug.LogError($"A Web exception occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"An exception occurred: {ex.Message}");
            }
        }

        return null;
    }
}
