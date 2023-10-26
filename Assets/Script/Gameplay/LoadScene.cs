using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    public float fadeDuration = 1.0f;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup != null)
        {
            StartCoroutine(FadeOutEffect());
        }
    }

    private IEnumerator FadeOutEffect()
    {
        float startAlpha = 1.0f;
        float targetAlpha = 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // Setelah fade out, pindah ke scene selanjutnya
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
