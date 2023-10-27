using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
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
    [SerializeField] private EventLog[] _eventLogs;
    private string _baseUrl = "http://localhost:3000/api/v1/auth/login";

    private UnityWebRequest LoginCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", _usernameInputField.text);

        byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(_passwordInputField.text);
        MD5 md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(passwordBytes);
        string hash = System.BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        form.AddField("password", hash);
        UnityWebRequest www = UnityWebRequest.Post(_baseUrl, form);
        return www;
    }

    IEnumerator HandleRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Account account = new Account("dummy", "dummy");
            CheckPrequelStatus(account);
            string json = JsonConvert.SerializeObject(account);
            File.WriteAllText(Application.persistentDataPath + "/account.json", json);
            _dummyAlert.gameObject.SetActive(true);
            PostSuccessLogin();
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log("Received: " + json);
            if (json == "{\"message\":\"Username or password is incorrect\"}")
            {
                Debug.Log("Username or password is incorrect");
            }
            else
            {
                Account account = JsonConvert.DeserializeObject<Account>(json);
                Debug.Log("Deserialized: " + account);
                if (account == null)
                {
                    Debug.Log("No data");
                    yield break;
                }
                else
                {
                    PostSuccessLogin();
                    Debug.Log("Login success");
                }
            }
        }
    }

    public void OnLoginButtonClicked()
    {
        StartCoroutine(HandleRequest(LoginCoroutine()));
    }
    public void PostSuccessLogin()
    {
        LogStatus logStatus = new LogStatus("login", "dummy");
        string json = JsonConvert.SerializeObject(logStatus);
        File.WriteAllText(Application.persistentDataPath + "/logstatus.json", json);
    }
    public void CheckPrequelStatus(Account account)
    {
        foreach (EventLog log in _eventLogs)
        {
            GameEvent gameEvent = new GameEvent(log.id_game, log.no_event);
            if (account.eventLogDict[gameEvent].status != log.status)
            {
                PrequelStatus prequelStatus = new PrequelStatus();
                prequelStatus.status = PrequelGameStatus.belum;
                File.WriteAllText(Application.persistentDataPath + "/prequelstatus.json", JsonConvert.SerializeObject(prequelStatus));
            }
            else
            {
                PrequelStatus prequelStatus = new PrequelStatus();
                prequelStatus.status = PrequelGameStatus.selesai;
                File.WriteAllText(Application.persistentDataPath + "/prequelstatus.json", JsonConvert.SerializeObject(prequelStatus));
            }
        }
    }
}
