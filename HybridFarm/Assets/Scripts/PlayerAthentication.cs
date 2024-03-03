using System.Collections;
using TMPro;
using UnityEngine;
using System.Net;
using System;

public class PlayerAuthentication : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    // Property to check if the user is authenticated
    public static bool IsAuthenticated { get; private set; }

    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        const string targetUrl = "http://google.com";
        const float checkInterval = 1f; // Check every 3 seconds

        bool isConnected = false;

        while (!isConnected) // Continue checking until the connection is established
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetUrl);
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                isConnected = true;
                action(true); // Connection established
            }
            catch (Exception)
            {
                // Connection failed, retry after a delay
                isConnected = false;
                action(false); // Connection failed
            }
            // Wait for 3 seconds before retrying
            yield return new WaitForSeconds(checkInterval);
        }
    }

    void Start()
    {
        StartCoroutine(CheckInternetConnection((isConnected) =>
        {
            if (isConnected)
            {
                debugText.text = "Internet connection established";
                StartCoroutine(AuthenticateAndSaveProfile());
            }
            else
            {
                debugText.text = "No Internet connection";
            }
        }));
    }

    IEnumerator AuthenticateAndSaveProfile()
    {
        // Authenticate the user
        string jwtKey = ApiController.GetJwtKey();

        if (jwtKey == null)
        {
            debugText.text = "Error occurred during JWT key retrieval";
            yield break; // Exit the coroutine on error
        }
        else
        {
            debugText.text = "JWT key retrieved successfully";
        }
        yield return new WaitForSeconds(1f);

        // Save the user profile
        UserProfile userProfile = ApiController.GetUserProfile(jwtKey);

        if (userProfile == null)
        {
            debugText.text = "Error occurred during user profile retrieval";
            yield break; // Exit the coroutine on error
        }
        else
        {
            debugText.text = "User profile retrieved successfully";
        }
        yield return new WaitForSeconds(1f);

        // Display the user profile
        debugText.text = $"Welcome {userProfile.FirstName} {userProfile.LastName}!";

        // Save the user profile data
        PlayerPrefs.SetString("firstName", userProfile.FirstName);
        PlayerPrefs.SetString("lastName", userProfile.LastName);
        PlayerPrefs.SetString("userName", userProfile.UserName);
        PlayerPrefs.SetString("nic", userProfile.Nic);
        PlayerPrefs.SetString("phoneNumber", userProfile.PhoneNumber);
        PlayerPrefs.SetString("email", userProfile.Email);
        PlayerPrefs.SetString("profilePictureUrl", userProfile.ProfilePictureUrl);

        // Set authentication status to true
        IsAuthenticated = true;
    }
}
