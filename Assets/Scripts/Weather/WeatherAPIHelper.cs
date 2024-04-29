using System.Net;
using System.IO;
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}");
            try
            {
                // Start the asynchronous operation
                using (WebResponse response = await request.GetResponseAsync().AsUniTask().AttachExternalCancellation(cts.Token))
                {
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string jsonResponse = await reader.ReadToEndAsync();
                        Debug.Log(jsonResponse);
                        return JsonUtility.FromJson<WeatherData>(jsonResponse);
                    }
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
