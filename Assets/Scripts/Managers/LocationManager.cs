using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LocationManager : MonoBehaviour
{
    private async void Start()
    {
        bool succeededRequest = false;
        UnityWebRequest request = await SetCoordinates(() => succeededRequest = true);

        if (!succeededRequest)
        {
            Debug.LogError("no coordinates received");
        }
    }

    private async UniTask<UnityWebRequest> SetCoordinates(Action onSuccess)
    {
        var request = UnityWebRequest.Get($"https://api.ipgeolocation.io/ipgeo?apiKey={ApiKeys.locationApiKey}");
        await request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.Success)
        {
            GeoLocationData geoLocationData = JsonUtility.FromJson<GeoLocationData>(request.downloadHandler.text);
            Debug.Log(geoLocationData.city);
            Debug.Log(geoLocationData.ip);

            WeatherAPIHelper.Latitude = geoLocationData.latitude;
            WeatherAPIHelper.Longitude = geoLocationData.longitude;

            onSuccess?.Invoke();

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
public class GeoLocationData
{
    public string ip;
    public string hostname;
    public string continent_code;
    public string continent_name;
    public string country_code2;
    public string country_code3;
    public string country_name;
    public string country_capital;
    public string state_prov;
    public string district;
    public string city;
    public string zipcode;
    public float latitude;
    public float longitude;
    public bool is_eu;
    public string calling_code;
    public string country_tld;
    public string languages;
    public string country_flag;
    public string geoname_id;
    public string isp;
    public string connection_type;
    public string organization;
    public string asn;
    public Currency currency;
    public TimeZoneInfo time_zone;
}

[Serializable]
public class Currency
{
    public string code;
    public string name;
    public string symbol;
}

[Serializable]
public class TimeZoneInfo
{
    public string name;
    public int offset;
    public string current_time;
    public double current_time_unix;
    public bool is_dst;
    public int dst_savings;
}

