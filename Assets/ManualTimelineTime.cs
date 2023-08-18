using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ManualTimelineTime : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private float timelineSpeed = 1.0f;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.RebuildGraph();
    }

    private void Update()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(timelineSpeed);
    }
}
