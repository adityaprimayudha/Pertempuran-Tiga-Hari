using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PostQuiz : MonoBehaviour
{
    private string id_game = "4";
    private string id_player;
    private string _quizUrl = "https://mtsnuhati.com/sigamingclub/api/inc/create_logquiz.php";
    [SerializeField] private GameObject _postQuizAlert;

    public void PostLogQuiz()
    {
        if (File.Exists(Application.persistentDataPath + "/account.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/account.json");
            Account account = JsonUtility.FromJson<Account>(json);
            id_player = account.id_player;
        }
        if (id_game != "" && id_player != "")
        {
            StartCoroutine(PostQuizCoroutine());
        }
    }

    IEnumerator PostQuizCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("id_game", id_game);
        form.AddField("id_player", id_player);
        form.AddField("score", QuizManager.GetInstance().totalCorrectAnswer / (QuizManager.GetInstance().totalCorrectAnswer + QuizManager.GetInstance().totalIncorrectAnswer) * 100);

        UnityWebRequest www = UnityWebRequest.Post(_quizUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error : " + www.error);
        }
        else
        {
            _postQuizAlert.SetActive(true);
        }
    }
}
