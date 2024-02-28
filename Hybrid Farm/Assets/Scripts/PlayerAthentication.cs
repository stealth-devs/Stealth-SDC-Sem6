using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAthentication : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    string jwtKey = null;

    // Start is called before the first frame update
    void Start()
    {
        jwtKey = ApiController.getJwtKey();
        while (jwtKey == null)
        {
            jwtKey = ApiController.getJwtKey();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveUserProfile()
    {
        UserProfile userProfile = ApiController.getUserProfile(jwtKey);
        PlayerPrefs.SetString("firstName", userProfile.firstName);
        PlayerPrefs.SetString("lastName", userProfile.lastName);
        PlayerPrefs.SetString("userName", userProfile.userName);
        PlayerPrefs.SetString("nic", userProfile.nic);
        PlayerPrefs.SetString("phoneNumber", userProfile.phoneNumber);
        PlayerPrefs.SetString("email", userProfile.email);
        PlayerPrefs.SetString("profilePictureUrl", userProfile.profilePictureUrl);
        PlayerPrefs.SetString("jwtKey", jwtKey);
    }
}
