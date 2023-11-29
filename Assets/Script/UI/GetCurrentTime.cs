using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrentTime : MonoBehaviour
{
    public static GetCurrentTime instance;
    public static DateTime startGame;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            startGame = DateTime.Now;
            Debug.Log("Start Game: " + startGame);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
