using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Linq;
using System;
using UnityEngine.Networking;
using PixelCrushers.DialogueSystem;

public class Logout : MonoBehaviour
{
    private string endGameTime;
    private string startGameTime;
    private string id_game = "4";
    private string id_player;
    private LogoutCallback logoutCallback = new LogoutCallback("", "", "");
    private string _logGameUrl = "https://mtsnuhati.com/sigamingclub/api/inc/create_loggame.php";
    private string _logGameEventUrl = "https://mtsnuhati.com/sigamingclub/api/inc/create_gameevent.php";
    List<string> entryKey = new List<string>();
    private string[] allQuests;
    private List<QuestStateProgress> questStateProgresses = new List<QuestStateProgress>();
    [SerializeField] private GameObject _logoutAlert;
    [SerializeField] private GameObject _logoutSuccess;
    public void OnLogoutButtonClicked()
    {
        _logoutAlert.SetActive(true);
        LogStatus logStatus = new LogStatus("logout", "");
        string json = JsonUtility.ToJson(logStatus);
        File.WriteAllText(Application.persistentDataPath + "/logstatus.json", json);
        PostLogGame();
    }
    private void ResetAccount()
    {
        if (File.Exists(Application.persistentDataPath + "/account.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/account.json");
            IDictionary<string, object> account = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            foreach (KeyValuePair<string, object> entry in account)
            {
                entryKey.Add(entry.Key);
            }
            foreach (string key in entryKey)
            {
                account[key] = "";
            }
            json = JsonConvert.SerializeObject(account);
            File.WriteAllText(Application.persistentDataPath + "/account.json", json);

            questStateProgresses.Clear();
        }
    }

    private void PostLogGame()
    {
        endGameTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        startGameTime = GetCurrentTime.startGame.ToString("yyyy-MM-dd HH:mm:ss");
        if (File.Exists(Application.persistentDataPath + "/account.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/account.json");
            Account account = JsonUtility.FromJson<Account>(json);
            id_player = account.id_player;
        }
        if (id_game != "" && id_player != "" && startGameTime != "" && endGameTime != "")
        {
            //Debug.Log("id_game : " + id_game + "\nid_player : " + id_player + "\nstart_time : " + startGameTime + "\nend_time : " + endGameTime);
            StartCoroutine(PostLogGameCoroutine());
        }
    }

    IEnumerator PostLogGameCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("id_game", id_game);
        form.AddField("id_player", id_player);
        form.AddField("waktu_mulai", startGameTime);
        form.AddField("waktu_entry", endGameTime);

        UnityWebRequest www = UnityWebRequest.Post(_logGameUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error : " + www.error);
        }
        else
        {
            logoutCallback = JsonConvert.DeserializeObject<LogoutCallback>(www.downloadHandler.text);
            CheckCurrentProgress();
        }
    }

    private void CheckCurrentProgress()
    {
        allQuests = QuestLog.GetAllQuests(QuestState.Success | QuestState.Active, false);
        for (int i = 0; i < allQuests.Length; i++)
        {
            Debug.Log(allQuests.Length);
            if (allQuests != null)
            {
                if (QuestLog.GetQuestState(allQuests[i]) == QuestState.Success)
                {
                    questStateProgresses.Add(new QuestStateProgress(i + 1, "selesai"));
                }
                else if (QuestLog.GetQuestState(allQuests[i]) == QuestState.Active)
                {
                    questStateProgresses.Add(new QuestStateProgress(i + 1, "sedang"));
                }
                else
                {
                    questStateProgresses.Add(new QuestStateProgress(i + 1, "belum"));
                }
            }
            else
            {
                Debug.Log("Tidak ada data quests");
            }
        }
        if (questStateProgresses != null)
        {
            PostLogGameEvent();
        }
    }

    private void PostLogGameEvent()
    {
        foreach (QuestStateProgress qsp in questStateProgresses)
        {
            // Debug.Log("QSP length : " + questStateProgresses.Count + "\nid_log : " + id_log + "\nno_event : " + qsp.no_event + "\nstatus_event : " + qsp.status_event);
            StartCoroutine(PostLogGameEventCoroutine(qsp));
        }
        _logoutAlert.SetActive(false);
        _logoutSuccess.SetActive(true);
    }

    IEnumerator PostLogGameEventCoroutine(QuestStateProgress qsp)
    {
        //Debug.Log("id_log : " + logoutCallback.id_log + "\nno_event : " + qsp.no_event + "\nstatus_event : " + qsp.status_event);
        WWWForm form = new WWWForm();
        form.AddField("id_log", logoutCallback.id_log);
        form.AddField("no_event", qsp.no_event);
        form.AddField("status_event", qsp.status_event);

        UnityWebRequest www = UnityWebRequest.Post(_logGameEventUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error : " + www.error);
        }
        else
        {
            Debug.Log("Success : " + www.downloadHandler.text);
            ResetAccount();
        }
    }
}

public class LogoutCallback
{
    public string status;
    public string message;
    public string id_log;

    public LogoutCallback(string status, string message, string id_log)
    {
        this.status = status;
        this.message = message;
        this.id_log = id_log;
    }
}

public class QuestStateProgress
{
    public int no_event;
    public string status_event;

    public QuestStateProgress(int no_event, string status_event)
    {
        this.no_event = no_event;
        this.status_event = status_event;
    }
}
