using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageVisibilityToggle : MonoBehaviour
{
    private Image image;
    private bool isVisible = true;
    public float interval = 0.5f; // Interval in seconds

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(ToggleVisibility());
    }

    IEnumerator ToggleVisibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            isVisible = !isVisible;
            image.enabled = isVisible;
        }
    }
}
