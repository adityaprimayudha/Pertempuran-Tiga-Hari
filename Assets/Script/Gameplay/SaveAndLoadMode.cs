using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
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
            DialogueLua.SetVariable("totalCorrectAnswers", (double)QuizManager.GetInstance().totalCorrectAnswer);
            DialogueLua.SetVariable("totalIncorrectAnswers", (double)QuizManager.GetInstance().totalIncorrectAnswer);
            saveSystemMethods.SaveSlot(2);
            File.WriteAllText(Application.persistentDataPath + "/saved.json", PlayerPrefs.GetString("Save2"));
            Debug.Log("Correct: " + DialogueLua.GetVariable("totalCorrectAnswers").asInt + "\nIncorrect: " + DialogueLua.GetVariable("totalIncorrectAnswers").asInt);
        }
        else
        {
            DialogueLua.SetVariable("totalCorrectAnswers", QuizManager.GetInstance().totalCorrectAnswer);
            DialogueLua.SetVariable("totalIncorrectAnswers", QuizManager.GetInstance().totalIncorrectAnswer);
            saveSystemMethods.SaveSlot(1);
            File.WriteAllText(Application.persistentDataPath + "/saved.json", PlayerPrefs.GetString("Save1"));
            Debug.Log("Correct: " + DialogueLua.GetVariable("totalCorrectAnswers").asInt + "\nIncorrect: " + DialogueLua.GetVariable("totalIncorrectAnswers").asInt);
        }
    }

    public void LoadType()
    {
        CheckAccountType();
        if (currentAccount.username == "dummy")
        {
            saveSystemMethods.LoadFromSlot(2);
            QuizManager.GetInstance().totalCorrectAnswer = DialogueLua.GetVariable("totalCorrectAnswers").asInt;
            QuizManager.GetInstance().totalIncorrectAnswer = DialogueLua.GetVariable("totalIncorrectAnswers").asInt;
            Debug.Log("Load from slot 2");
        }
        else
        {
            saveSystemMethods.LoadFromSlot(1);
            QuizManager.GetInstance().totalCorrectAnswer = DialogueLua.GetVariable("totalCorrectAnswers").asInt;
            QuizManager.GetInstance().totalIncorrectAnswer = DialogueLua.GetVariable("totalIncorrectAnswers").asInt;
            Debug.Log("Load from slot 1");
            Debug.Log("Correct: " + QuizManager.GetInstance().totalCorrectAnswer + "\nIncorrect: " + QuizManager.GetInstance().totalIncorrectAnswer);
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
