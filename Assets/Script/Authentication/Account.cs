using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    public string username;
    public string id;
    public EventLog[] eventLogs;
    public Dictionary<GameEvent, EventLog> eventLogDict = new Dictionary<GameEvent, EventLog>();

    public Account(string username, string id)
    {
        this.username = username;
        this.id = id;

        //DummyData
        eventLogs = new EventLog[6];
        eventLogs[0] = new EventLog(1, 1, EventStatus.selesai);
        eventLogs[1] = new EventLog(1, 2, EventStatus.selesai);
        eventLogs[2] = new EventLog(1, 3, EventStatus.selesai);
        eventLogs[3] = new EventLog(2, 1, EventStatus.selesai);
        eventLogs[4] = new EventLog(2, 2, EventStatus.selesai);
        eventLogs[5] = new EventLog(2, 3, EventStatus.belum);
        //end of DummyData

        foreach (EventLog eventLog in eventLogs)
        {
            eventLogDict.Add(new GameEvent(eventLog.id_game, eventLog.no_event), eventLog);
        }
    }

}
public struct GameEvent
{
    public int id_game;
    public int no_event;

    public GameEvent(int id_game, int no_event)
    {
        this.id_game = id_game;
        this.no_event = no_event;
    }
}
