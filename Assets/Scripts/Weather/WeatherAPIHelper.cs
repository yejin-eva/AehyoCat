using System.Net;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public static class WeatherAPIHelper
{
    static readonly string apiKey = "e17ce596ed8f254331cf07a3ca5ea190";
    public static async Task<WeatherData> GetWeatherData()
    {
        float latitude = 37.568291f;
        float longitude = 126.997780f;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}");
        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonResponse = await reader.ReadToEndAsync();
                    Debug.Log(jsonResponse);
                    return JsonUtility.FromJson<WeatherData>(jsonResponse);
                }
            }
            else
            {
                // 오류 처리 로직
            }
        }

        return null;
    }
}