using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerAuthentication : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    // Property to check if the user is authenticated
    public static bool IsAuthenticated { get; private set; }

    void Start()
    {
        StartCoroutine(AuthenticateAndSaveProfile());
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

        // Save the user profile
        UserProfile userProfile = ApiController.GetUserProfile(jwtKey);

        if (userProfile == null)
        {
            debugText.text = "Error occurred during user profile retrieval";
            yield break; // Exit the coroutine on error
        }

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
