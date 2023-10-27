using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Linq;

public class Logout : MonoBehaviour
{
    List<string> entryKey = new List<string>();
    public void OnLogoutButtonClicked()
    {
        LogStatus logStatus = new LogStatus("logout", "");
        string json = JsonUtility.ToJson(logStatus);
        File.WriteAllText(Application.persistentDataPath + "/logstatus.json", json);
        ResetAccount();
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
        }
    }
}
