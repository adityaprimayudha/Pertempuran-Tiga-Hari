using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;
    [SerializeField] private GameObject _dummyAlert;
    [SerializeField] private GameObject _successAlert;
    [SerializeField] private GameObject _tryAgainAlert;
    [SerializeField] private EventLog[] _eventLogs;
    private string _baseUrl = "https://mtsnuhati.com/sigamingclub/api/inc/loginPlayer.php";

    private UnityWebRequest LoginCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", _usernameInputField.text);

        byte[] passwordBytes = Encoding.UTF8.GetBytes(_passwordInputField.text);
        MD5 md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(passwordBytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        form.AddField("password", hash);
        UnityWebRequest www = UnityWebRequest.Post(_baseUrl, form);
        return www;
    }

    IEnumerator HandleRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            // Account account = new Account("dummy", "dummy");
            // CheckPrequelStatus(account);
            // string json = JsonConvert.SerializeObject(account);
            // File.WriteAllText(Application.persistentDataPath + "/account.json", json);
            // _dummyAlert.gameObject.SetActive(true);
            // PostSuccessLogin("Dummy");
            _tryAgainAlert.gameObject.SetActive(true);
        }
        else
        {
            LoginCallback loginCallback = JsonConvert.DeserializeObject<LoginCallback>(www.downloadHandler.text);
            if (loginCallback.status == "200")
            {
                //Decode Base64String
                string token = loginCallback.token;
                string[] tokenParts = token.Split('.');
                string payload = tokenParts[1];
                byte[] decodedPayload = Convert.FromBase64String(PayloadBase64Validate(payload));
                string decodedPayloadString = Encoding.UTF8.GetString(decodedPayload);
                JWTData jwtData = JsonConvert.DeserializeObject<JWTData>(decodedPayloadString);

                //Get EventLog List
                List<List<EventLog>> playerProgression = loginCallback.playerProgression;
                List<EventLog> eventLogs = new List<EventLog>();
                foreach (List<EventLog> eventDataList in playerProgression)
                {
                    foreach (EventLog eventData in eventDataList)
                    {
                        eventLogs.Add(eventData);
                    }
                }

                Debug.Log("Total : " + eventLogs.Count);
                foreach (EventLog log in eventLogs)
                {
                    Debug.Log("id_game : " + log.id_game + "\nid_log : " + log.id_log + "\nno_event : " + log.no_event + "\nevent_status : " + log.status);
                }

                // Save decoded data into Account 
                Account account = new Account(jwtData.data.username, jwtData.data.id_player, eventLogs);
                string json = JsonConvert.SerializeObject(account);
                File.WriteAllText(Application.persistentDataPath + "/account.json", json);
                CheckPrequelStatus(account);

                _successAlert.gameObject.SetActive(true);
                PostSuccessLogin(jwtData.data.username);
            }
        }
    }
    private string PayloadBase64Validate(string payload)
    {
        while (payload.Length % 4 != 0)
        {
            payload += "=";
        }
        return payload;
    }

    public void OnLoginButtonClicked()
    {
        StartCoroutine(HandleRequest(LoginCoroutine()));
    }


    public void PostSuccessLogin(string status)
    {
        if (status == "Dummy")
        {
            LogStatus logStatus = new LogStatus("login", "dummy");
            string json = JsonConvert.SerializeObject(logStatus);
            File.WriteAllText(Application.persistentDataPath + "/logstatus.json", json);
        }
        else
        {
            LogStatus logStatus = new LogStatus("login", status);
            string json = JsonConvert.SerializeObject(logStatus);
            File.WriteAllText(Application.persistentDataPath + "/logstatus.json", json);
        }
    }
    public void CheckPrequelStatus(Account account)
    {
        PrequelStatus status = new PrequelStatus();
        foreach (EventLog log in _eventLogs)
        {
            GameEvent gameEvent = new GameEvent(log.id_game, log.no_event);
            if (!account.eventLogDict.ContainsKey(gameEvent))
            {
                status.status = PrequelGameStatus.belum;
                break;
            }
            else
            {
                if (account.eventLogDict[gameEvent].status != log.status)
                {
                    status.status = PrequelGameStatus.belum;
                    break;
                }
                else
                {
                    status.status = PrequelGameStatus.selesai;
                }
            }
        }
        File.WriteAllText(Application.persistentDataPath + "/prequelstatus.json", JsonConvert.SerializeObject(status));
    }

    [Serializable]
    public class LoginCallback
    {
        public string status;
        public string message;
        public string token;
        public List<List<EventLog>> playerProgression;
    }

    // [Serializable]
    // public class EventData
    // {
    //     public string id_log;
    //     public string no_event;
    //     public string status_event;
    //     public string id_game;
    // }

    [Serializable]
    public class JWTData
    {
        public string iss;
        public string iat;
        public string exp;
        public DecodedData data;

        public class DecodedData
        {
            public string id_player;
            public string nama_player;
            public string username;
        }
    }
}
