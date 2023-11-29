using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PrequelStatusAlert : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alertText;
    private void OnEnable()
    {
        if (File.Exists(Application.persistentDataPath + "/prequelstatus.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/prequelstatus.json");
            PrequelStatus prequelStatus = JsonUtility.FromJson<PrequelStatus>(json);
            if (prequelStatus.status == PrequelGameStatus.belum)
            {
                _alertText.text = "Prequel Status!\n<color=white>Sepertinya kamu belum selesai atau belum memainkan game Sebelumnya";
            }
            else
            {
                _alertText.text = "Prequel Status!\n<color=white>Selamat kamu sudah menyelesaikan game-game Sebelumnya\nRingkasan cerita akan ditampilkan di awal game";
            }
        }
    }
}
