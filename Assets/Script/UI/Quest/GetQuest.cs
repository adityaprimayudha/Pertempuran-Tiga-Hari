using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using TMPro;

public class GetQuest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quest;
    [SerializeField] private TextMeshProUGUI questDescription;
    private string[] allQuests;

    private void OnEnable()
    {
        Lua.RegisterFunction(nameof(GetQuests), this, SymbolExtensions.GetMethodInfo(() => GetQuests()));
    }
    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(GetQuests));
    }

    public void GetQuests()
    {
        allQuests = QuestLog.GetAllQuests();
        if (allQuests.Length != 0)
        {
            quest.text = allQuests[0];
            questDescription.text = QuestLog.GetQuestDescription(allQuests[0]);
            DialogueManager.ShowAlert("Quest Updated\nTekan Esc untuk melihat Quest");
            Debug.Log("Quest Updated");
        }
        else
        {
            Debug.Log("No Quests");
        }
    }
}
