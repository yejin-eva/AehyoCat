using System.Net;
using UnityEngine;
using Cysharp.Threading.Tasks; 
using System.Threading;
using System;
using UnityEngine.Networking;

public static class WeatherAPIHelper
{
    public static Action OnTimeOut;
    static readonly string apiKey = "e17ce596ed8f254331cf07a3ca5ea190";
    static int cancelTime = 3000; // Milliseconds

    public static async UniTask<WeatherData> GetWeatherData()
    {
        float latitude = 37.568291f;
        float longitude = 126.997780f;

        using (CancellationTokenSource cts = new CancellationTokenSource(cancelTime))
        {
            var request = UnityWebRequest.Get($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}");
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
