using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private void Awake()
    {
        string dbName = "/AehyoCatApiKeys.db";
        string connectionSTring = "URI=file:" + Application.streamingAssetsPath + dbName;
        IDbConnection dbConnection = new SqliteConnection(connectionSTring);
        dbConnection.Open();

        string tableName = "apiKeys";

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tableName;
        IDataReader reader = dbCommand.ExecuteReader();

        if (reader.Read())
        {
            ApiKeys.LocationApiKey = reader.GetString(2);
            Debug.Log("LocationApiKey: " + ApiKeys.LocationApiKey);
        }

        if (reader.Read())
        {
            ApiKeys.WeatherApiKey = reader.GetString(2);
            Debug.Log("WeatherApiKey: " + ApiKeys.WeatherApiKey);
        }

        if (reader.Read())
        {
            ApiKeys.ServerKey = reader.GetString(2);
            Debug.Log("ServerKey: " + ApiKeys.ServerKey);
        }

        if (reader.Read())
        {
            ApiKeys.Issuer = reader.GetString(2);
            Debug.Log("Issuer: " + ApiKeys.Issuer);
        }
        if (reader.Read())
        {
               ApiKeys.Domain = reader.GetString(2);
            Debug.Log("Domain: " + ApiKeys.Domain);
        }
        if (reader.Read())
        {
            ApiKeys.TokenKey = reader.GetString(2);
            Debug.Log("TokenKey: " + ApiKeys.TokenKey);
        }

        
    }
}
