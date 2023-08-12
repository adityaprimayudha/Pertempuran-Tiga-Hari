using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private string _spawnPointName;
    [SerializeField] private GameObject _player;
    void Awake()
    {
        bool hasSpawnPoint = PixelCrushers.DialogueSystem.DialogueLua.GetVariable(_spawnPointName).asBool;
        GameObject Timeline = GameObject.FindGameObjectWithTag("Timeline");
        if (!hasSpawnPoint)
        {
            Timeline.SetActive(false);
            _player.transform.position = transform.position;
        }
        else
        {
            Timeline.SetActive(true);
        }

    }
}
