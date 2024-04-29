using System.Net;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using System;
using Unity.VisualScripting;

public static class WeatherAPIHelper
{
    public static Action OnTimeOut;
    static readonly string apiKey = "e17ce596ed8f254331cf07a3ca5ea190";

    static int cancelTime = 3000; //milliseconds

    public static async Task<WeatherData> GetWeatherData()
    {
        float latitude = 37.568291f;
        float longitude = 126.997780f;

        using (CancellationTokenSource cts = new CancellationTokenSource(cancelTime))
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}");

            try
            {
                // Start the asynchronous operation
                using (WebResponse response = await request.GetResponseAsync().WithCancellation(cts.Token))
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

public static class TaskExtensions
{
    // Extension method to handle cancellation of asynchronous web requests
    public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<bool>();
        using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
        {
            if (task != await Task.WhenAny(task, tcs.Task))
            {
                throw new OperationCanceledException(cancellationToken);
            }
        }

        return await task;
    }
}