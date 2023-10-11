using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;

public class AssignConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D confiner;
    // Start is called before the first frame update
    void Awake()
    {
        AssignConfine();
        AssignFollow();
    }

    private void AssignConfine()
    {
        bool isNull = confiner.m_BoundingShape2D == null;
        confiner.m_BoundingShape2D = GameObject.FindWithTag("Confiner").GetComponent<Collider2D>();
        Debug.Log(confiner.m_BoundingShape2D);
        if (GameObject.FindWithTag("Confiner").GetComponent<Collider2D>() == null)
            Debug.LogError("No confiner found");

        if (isNull)
        {
            confiner.InvalidateCache();
        }
    }

    private void AssignFollow()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
            Debug.LogError("No player found");
        else
        {
            GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        }
    }
}
