using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(DelayedLoadNextScene());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(WaitForAuthenticationAndLoadNextScene());
        }
    }

    IEnumerator DelayedLoadNextScene()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        LoadNextScene();
    }

    IEnumerator WaitForAuthenticationAndLoadNextScene()
    {
        while (!PlayerAuthentication.IsAuthenticated)
        {
            yield return null; // Wait until authenticated
        }

        yield return new WaitForSeconds(3f); // Additional delay (optional)

        LoadNextScene();
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneWithTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        PlayTransitionAnimation();

        yield return new WaitForSeconds(transitionTime); // Wait for transition animation

        SceneManager.LoadScene(sceneIndex);
    }

    void PlayTransitionAnimation()
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
        }
        else
        {
            Debug.LogError("Transition animator is not assigned.");
        }
    }
}
