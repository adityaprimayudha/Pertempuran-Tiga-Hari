using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    public string username;
    public string id_player;
    public List<EventLog> eventLogs;
    public Dictionary<GameEvent, EventLog> eventLogDict = new Dictionary<GameEvent, EventLog>();

    public Account(string username, string id_player, List<EventLog> eventLogs)
    {
        this.username = username;
        this.id_player = id_player;
        this.eventLogs = eventLogs;
        foreach (EventLog eventLog in eventLogs)
        {
            //Debug.Log("id_game : " + eventLog.id_game + "\nno_event : " + eventLog.no_event + "\nevent_status : " + eventLog.status);
            eventLogDict.Add(new GameEvent(eventLog.id_game, eventLog.no_event), eventLog);
        }
    }
    // // {
    // this.username = username;
    // this.id = id;

    // //DummyData
    // eventLogss = new EventLog[6];
    // eventLogss[0] = new EventLog(1, 10, 1, EventStatus.selesai);
    // eventLogss[1] = new EventLog(1, 12, 2, EventStatus.selesai);
    // eventLogss[2] = new EventLog(1, 11, 3, EventStatus.selesai);
    // eventLogss[3] = new EventLog(2, 9, 1, EventStatus.selesai);
    // eventLogss[4] = new EventLog(2, 8, 2, EventStatus.selesai);
    // eventLogss[5] = new EventLog(2, 17, 3, EventStatus.belum);
    // //end of DummyData

    // foreach (EventLog eventLog in eventLogss)
    // {
    //     eventLogDict.Add(new GameEvent(eventLog.id_game, eventLog.no_event), eventLog);
    // }
    // }

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
