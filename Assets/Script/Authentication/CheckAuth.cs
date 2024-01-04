using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using PixelCrushers;
using UnityEngine.UI;

public class CheckAuth : MonoBehaviour
{
    [SerializeField] private GameObject _loginButton;
    [SerializeField] private GameObject _logoutButton;
    [SerializeField] private TextMeshProUGUI _usernameText;
    [SerializeField] private Button _loadButton;
    private string json;
    private LogStatus logStatus;
    [SerializeField] private PlayerPrefsSavedGameDataStorer _playerPrefs;

    private void Awake()
    {
        CheckFile();
        CheckSaveData();
        ReadData();
    }

    public void CheckFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/prequelstatus.json"))
        {
            PrequelStatus prequelStatus = new PrequelStatus();
            prequelStatus.status = PrequelGameStatus.belum;
            string json = JsonUtility.ToJson(prequelStatus);
            File.WriteAllText(Application.persistentDataPath + "/prequelstatus.json", json);
        }
    }

    public void CheckSaveData()
    {
        if (_playerPrefs == null)
        {
            _playerPrefs = FindObjectOfType<PlayerPrefsSavedGameDataStorer>();
        }
        if (File.Exists(Application.persistentDataPath + "/logstatus.json"))
        {
            json = File.ReadAllText(Application.persistentDataPath + "/logstatus.json");
            logStatus = JsonUtility.FromJson<LogStatus>(json);
            if (logStatus.status == "login")
            {
                _playerPrefs.playerPrefsKeyBase = logStatus.username;
                Debug.Log("Key: " + _playerPrefs.playerPrefsKeyBase);
            }
            else
            {
                _playerPrefs.playerPrefsKeyBase = "Save";
            }
        }
        else
        {
            _playerPrefs.playerPrefsKeyBase = "Save";
        }
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
        if (SaveSystem.HasSavedGameInSlot(1) || SaveSystem.HasSavedGameInSlot(2))
        {
            _loadButton.interactable = true;
        }
        else
        {
            _loadButton.interactable = false;
        }
    }
}
