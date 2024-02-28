using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // Load the main menu
            StartCoroutine(DelayedLoadNextScene());
        }
    }

    IEnumerator DelayedLoadNextScene()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Load the main menu
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        StartCoroutine(
                // Load the main menu
                LoadMenu(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadMenu(int sceneIndex)
    {
        // Play the animation
        transition.SetTrigger("Start");

        // Wait for 3 seconds
        yield return new WaitForSeconds(transitionTime);

        // Load the main menu
        SceneManager.LoadScene(sceneIndex);
    }
}
