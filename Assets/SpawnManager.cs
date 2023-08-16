using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private string _spawnPointName;
    private GameObject _player;
    void Start()
    {
        bool hasSpawnPoint = PixelCrushers.DialogueSystem.DialogueLua.GetVariable(_spawnPointName).asBool;
        _player = GameObject.FindGameObjectWithTag("PlayerSpawn");
        GameObject Timeline = GameObject.FindGameObjectWithTag("Timeline");
        if (!hasSpawnPoint)
        {
            Timeline.SetActive(false);
            _player.transform.position = transform.position;
        }
        else
        {
            PixelCrushers.DialogueSystem.DialogueLua.SetVariable("InCutscene", true);
            Timeline.SetActive(true);
        }
        if (PixelCrushers.DialogueSystem.DialogueLua.GetVariable("InCutscene").asBool && gameObject.name != "SpawnManager")
        {
            Debug.Log("InCutscene");
            _player.transform.position = transform.position;
            PixelCrushers.DialogueSystem.DialogueLua.SetVariable("InCutscene", false);
            Debug.Log(_player.transform.position + "" + transform.position);
        }
    }
}
