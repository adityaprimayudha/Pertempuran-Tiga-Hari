using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventLog
{
    public int id_game;
    public int id_log;
    public int no_event;
    public EventStatus status;

    public EventLog(int id_game, int no_event, EventStatus status)
    {
        this.id_game = id_game;
        this.no_event = no_event;
        this.status = status;
    }
}