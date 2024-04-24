using System.Net;
using System.IO;
using UnityEngine;

public static class WeatherAPIHelper
{
    static readonly string apiKey = "e17ce596ed8f254331cf07a3ca5ea190";
    public static WeatherData GetWeatherData()
    {
        float latitude = 37.568291f;
        float longitude = 126.997780f;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}");

        //request.UseDefaultCredentials = true;
        //request.PreAuthenticate = true;
        //request.Credentials = CredentialCache.DefaultCredentials;

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        
        Debug.Log(jsonResponse);
        
        return JsonUtility.FromJson<WeatherData>(jsonResponse);

    }
}