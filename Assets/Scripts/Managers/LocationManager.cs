using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LocationManager : MonoBehaviour
{
    private string IPAddress;
    Action OnSuccess;
    private async void Start()
    {
        //StartCoroutine(GetCoordinates());
        bool succeededIPFetch = false;
        UnityWebRequest request = await GetIP(() => succeededIPFetch = true);
        if (succeededIPFetch)
        {
            IPAddress = request.downloadHandler.text;
            Debug.Log(IPAddress);

            var request2 = await GetCoordinates();
        }
        else
        {
            Debug.LogError("no IP address received");
        }



    }

    private static async UniTask<UnityWebRequest> GetIP(Action onSuccess)
    {
        var request = UnityWebRequest.Get("https://api.ipify.org/");
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke();
            return request;
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
        return null;
    }
    private async UniTask<UnityWebRequest> GetCoordinates()
    {
        var request = UnityWebRequest.Get($"https://ipapi.co/json/");
        await request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.Success)
        {
            LocationInfo locationInfo = JsonUtility.FromJson<LocationInfo>(request.downloadHandler.text);
            Debug.Log(locationInfo.city);
            Debug.Log(locationInfo.ip);

            WeatherAPIHelper.Latitude = locationInfo.latitude;
            WeatherAPIHelper.Longitude = locationInfo.longitude;

            return request;
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }

        return null;
    }
    
}

[Serializable]
public class LocationInfo
{
    public string ip;
    public string city;
    public string region;
    public string region_code;
    public string country;
    public string country_name;
    public string continent_code;
    public bool in_eu;
    public float latitude;
    public float longitude;
    public string timezone;
    public string utc_offset;
    public string country_calling_code;
    public string currency;
    public string languages;
    public string asn;
    public string org;
}
