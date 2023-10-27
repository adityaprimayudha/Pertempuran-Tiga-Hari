using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GET : MonoBehaviour
{
    private string json;
    [SerializeField] private TMP_InputField inputField;
    private List<Games> games;
    IEnumerator SendGetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                json = webRequest.downloadHandler.text;
                File.WriteAllText(Application.dataPath + "/Script/API/Get.json", json);
                Debug.Log("Received: " + json);
            }
        }
    }

    private void DeserializeJSON()
    {
        json = File.ReadAllText(Application.dataPath + "/Script/API/Get.json");
        games = JsonConvert.DeserializeObject<List<Games>>(json);
        Debug.Log("Deserialized: " + games);
        if (games == null)
        {
            Debug.Log("No data");
            return;
        }
        else
        {
            string id = inputField.text;
            foreach (Games game in games)
            {
                if (game.id == id)
                {
                    Debug.Log("ID : " + game.id + "\nName: " + game.name + "\nGenre: " + game.genre[0] + "\nDeveloper: " + game.developers[0] + "\nPublisher: " + game.publishers[0] + "\nJapan: " + game.releaseDates.Japan + "\nEurope: " + game.releaseDates.Europe + "\nNorth America: " + game.releaseDates.NorthAmerica + "\nAustralia: " + game.releaseDates.Australia);
                }
                else
                {
                    //Debug.Log("No data");
                }
            }
        }
    }

    public void GetRequest()
    {
        string url = "https://api.sampleapis.com/switch/games";
        StartCoroutine(SendGetRequest(url));
    }
    public void FilterData()
    {
        DeserializeJSON();
    }

    public class Games
    {
        public string id;
        public string name;
        public string[] genre;
        public string[] developers;
        public string[] publishers;
        public ReleasedDates releaseDates;
    }

    public class ReleasedDates
    {
        public string Japan;
        public string NorthAmerica;
        public string Europe;
        public string Australia;
    }
}
