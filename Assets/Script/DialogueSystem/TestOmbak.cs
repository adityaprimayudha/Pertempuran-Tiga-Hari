using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using UnityEngine.SceneManagement;

public class TestOmbak : MonoBehaviour
{
    private bool isKeluar;
    public string nextScene;
    // Update is called once per frame
    void Update()
    {
        isKeluar = PixelCrushers.DialogueSystem.DialogueLua.GetVariable("keluarRumah").asBool;
        if (isKeluar)
        {
            SceneManager.LoadScene(nextScene);
            OnDisable();
        }
        else
        {
            Debug.Log("Belum Keluar");
        }
    }

    public void OnDisable()
    {
        PixelCrushers.DialogueSystem.DialogueLua.SetVariable("keluarRumah", false);
        this.GetComponent<TestOmbak>().enabled = false;
    }
}
