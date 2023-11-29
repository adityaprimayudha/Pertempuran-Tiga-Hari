using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private void CheckStatus()
    {
        if (File.Exists(Application.persistentDataPath + "/prequelstatus.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/prequelstatus.json");
            PrequelStatus prequelStatus = JsonUtility.FromJson<PrequelStatus>(json);
            if (prequelStatus.status == PrequelGameStatus.selesai)
            {
                SceneManager.LoadScene("Summary");
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
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
        CheckStatus();
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
