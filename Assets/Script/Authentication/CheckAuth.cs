using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.IO;

public class CheckAuth : MonoBehaviour
{
    [SerializeField] private GameObject _loginButton;
    [SerializeField] private GameObject _logoutButton;
    [SerializeField] private TextMeshProUGUI _usernameText;
    private string json;
    private LogStatus logStatus;

    private void Awake()
    {
        ReadData();
    }

    public void ReadData()
    {
        if (File.Exists(Application.persistentDataPath + "/logstatus.json"))
        {
            json = File.ReadAllText(Application.persistentDataPath + "/logstatus.json");
            logStatus = JsonUtility.FromJson<LogStatus>(json);
            if (logStatus.status == "login")
            {
                _loginButton.gameObject.SetActive(false);
                _logoutButton.gameObject.SetActive(true);
                _usernameText.gameObject.SetActive(true);
                _usernameText.text = "Welcome, " + logStatus.username;
            }
            else
            {
                _loginButton.gameObject.SetActive(true);
                _logoutButton.gameObject.SetActive(false);
                _usernameText.gameObject.SetActive(false);
            }
        }
        else
        {
            _loginButton.gameObject.SetActive(true);
            _logoutButton.gameObject.SetActive(false);
            _usernameText.gameObject.SetActive(false);
        }
    }
}
