using System.Collections;
using System.Collections.Generic;
using System.IO;
using PixelCrushers;
using PixelCrushers.Wrappers;
using UnityEngine;

public class SaveAndLoadMode : MonoBehaviour
{
    private Account currentAccount;
    private PixelCrushers.SaveSystemMethods saveSystemMethods = new PixelCrushers.SaveSystemMethods();
    public void SaveType()
    {
        CheckAccountType();
        if (currentAccount.username == "dummy")
        {
            saveSystemMethods.SaveSlot(2);
            File.WriteAllText(Application.persistentDataPath + "/saved.json", PlayerPrefs.GetString("Save2"));
        }
        else
        {
            saveSystemMethods.SaveSlot(1);
            File.WriteAllText(Application.persistentDataPath + "/saved.json", PlayerPrefs.GetString("Save1"));
        }
    }

    public void LoadType()
    {
        CheckAccountType();
        if (currentAccount.username == "dummy")
        {
            saveSystemMethods.LoadFromSlot(2);
            Debug.Log("Load from slot 2");
        }
        else
        {
            saveSystemMethods.LoadFromSlot(1);
            Debug.Log("Load from slot 1");
        }
    }

    public void CheckAccountType()
    {
        if (File.Exists(Application.persistentDataPath + "/account.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/account.json");
            currentAccount = JsonUtility.FromJson<Account>(json);

        }
    }
}
