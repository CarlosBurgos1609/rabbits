using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public Image storyImage;
    public Sprite[] storyFrames; // Array con las im�genes de la historia
    public float frameDuration = 3f; // Duraci�n de cada imagen en segundos
    public float fadeDuration = 1f; // Duraci�n de la transici�n

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = storyImage.gameObject.AddComponent<CanvasGroup>(); // A�adir Canvas Group para la transici�n
        StartCoroutine(PlayStory());
    }

    IEnumerator PlayStory()
    {
        foreach (Sprite frame in storyFrames)
        {
            yield return StartCoroutine(FadeImage(frame)); // Llamar la animaci�n de fade
            yield return new WaitForSeconds(frameDuration);
        }

        // Cuando termine la historia, carga el Men� Principal
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeImage(Sprite newImage)
    {
        // Fundido a negro
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;

        // Cambiar imagen
        storyImage.sprite = newImage;

        // Fundido a la imagen nueva
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}

