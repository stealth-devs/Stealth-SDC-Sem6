using TMPro;
using UnityEngine;

public class DebugApi : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    string jwtKey;

    void Start()
    {
        jwtKey = ApiController.getJwtKey();
    }

    public void ShowUserProfile()
    {
        //string jwtKey = ApiController.getJwtKey();
        //string jwtKey = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJvdmVyc2lnaHRfZzkiLCJpYXQiOjE3MDkwNTExODIsImV4cCI6MTcwOTA4NzE4Mn0.XJYf6vCMviKNzhCIaMcdpYeg7i5BZ-ulv3sCNXoivrmvWDldeoRQF_rGbNjVzB5WHq0BecJhE_yEKMnz1luIoQ";
        UserProfile userProfile = ApiController.getUserProfile(jwtKey);
        debugText.text = "First Name: " + userProfile.firstName + "\n" +
                         "Last Name: " + userProfile.lastName + "\n" +
                         "User Name: " + userProfile.userName + "\n" +
                         "NIC: " + userProfile.nic + "\n" +
                         "Phone Number: " + userProfile.phoneNumber + "\n" +
                         "Email: " + userProfile.email + "\n" +
                         "Profile Picture URL: " + userProfile.profilePictureUrl + "\n" +
                         "JWT Key: " + jwtKey;
    }

    public void ShowJwtKey()
    {
        string jwtKey = ApiController.getJwtKey();
        debugText.text = "JWT Key: " + jwtKey;
    }
}
