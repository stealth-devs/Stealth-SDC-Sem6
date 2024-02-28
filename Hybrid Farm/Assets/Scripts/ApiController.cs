using UnityEngine;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

public static class ApiController
{

    public static string getJwtKey()
    {
        string url = "http://20.15.114.131:8080/api/login";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Accept = "*/*";
        string body = "{\"apiKey\":\"NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNlOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNA\"}";
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(body);
            streamWriter.Flush();
            streamWriter.Close();
        }
        try
        {
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using StreamReader reader = new(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            JObject jsonObject = JObject.Parse(jsonResponse);
            string token = (string)jsonObject["token"];
            return token;
        }
        catch (WebException ex)
        {
            Debug.LogError($"Error occurred during JWT key retrieval: {ex.Message}");
            return null;
        }
    }

    public static UserProfile getUserProfile(string jwtKey)
    {
        string url = "http://20.15.114.131:8080/api/user/profile/view";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        request.Headers.Add("Authorization", "Bearer " + jwtKey);
        try
        {
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using StreamReader reader = new(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            JObject jsonObject = JObject.Parse(jsonResponse);

            UserProfile userProfile = new()
            {
                firstName = (string)jsonObject["user"]["firstname"],
                lastName = (string)jsonObject["user"]["lastname"],
                userName = (string)jsonObject["user"]["username"],
                nic = (string)jsonObject["user"]["nic"],
                phoneNumber = (string)jsonObject["user"]["phoneNumber"],
                email = (string)jsonObject["user"]["email"],
                profilePictureUrl = (string)jsonObject["user"]["profilePictureUrl"]
            };

            return userProfile;
        }
        catch (WebException ex)
        {
            Debug.LogError($"Error occurred during user profile retrieval: {ex.Message}");
            return null;
        }
    }
}
