using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        ChooseBGM();
    }
    private void ChooseBGM()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                BGMManager.Instance.PlayBGMMenu();
                break;
            case "Indoor":
                BGMManager.Instance.PlayBGMIndoor();
                break;
            case "Outdoor":
                BGMManager.Instance.PlayBGMOutdoor();
                break;
            case "Pelabuhan":
                BGMManager.Instance.PlayBGMPelabuhan();
                break;
            case "BalaiKota":
                BGMManager.Instance.PlayBGMBalaiKota();
                break;
            case "Penyerbuan":
                BGMManager.Instance.PlayBGMPenyerbuan();
                break;
            case "GedungInternatio":
                BGMManager.Instance.PlayBGMGedungInternatio();
                break;
        }
    }

}
