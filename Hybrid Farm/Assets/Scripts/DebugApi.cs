using TMPro;
using UnityEngine;

public class DebugApi : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    string jwtKey;

    void Start()
    {
        jwtKey = ApiController.GetJwtKey();
    }

    public void ShowUserProfile()
    {
        //string jwtKey = ApiController.getJwtKey();
        //string jwtKey = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJvdmVyc2lnaHRfZzkiLCJpYXQiOjE3MDkwNTExODIsImV4cCI6MTcwOTA4NzE4Mn0.XJYf6vCMviKNzhCIaMcdpYeg7i5BZ-ulv3sCNXoivrmvWDldeoRQF_rGbNjVzB5WHq0BecJhE_yEKMnz1luIoQ";
        UserProfile userProfile = ApiController.GetUserProfile(jwtKey);
        debugText.text = "First Name: " + userProfile.FirstName + "\n" +
                         "Last Name: " + userProfile.LastName + "\n" +
                         "User Name: " + userProfile.UserName + "\n" +
                         "NIC: " + userProfile.Nic + "\n" +
                         "Phone Number: " + userProfile.PhoneNumber + "\n" +
                         "Email: " + userProfile.Email + "\n" +
                         "Profile Picture URL: " + userProfile.ProfilePictureUrl + "\n" +
                         "JWT Key: " + jwtKey;
    }

    public void ShowJwtKey()
    {
        string jwtKey = ApiController.GetJwtKey();
        debugText.text = "JWT Key: " + jwtKey;
    }
}
