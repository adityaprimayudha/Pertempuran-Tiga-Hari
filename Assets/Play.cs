using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Play : MonoBehaviour
{
    [SerializeField] private GameObject enemyObjects;
    [SerializeField] private GameObject npc;
    private EnemyController[] control;
    private Shoot[] shoot;

    void Awake()
    {
        control = enemyObjects.GetComponentsInChildren<EnemyController>();
        shoot = npc.GetComponentsInChildren<Shoot>();
    }

    void OnEnable()
    {
        Lua.RegisterFunction(nameof(DisableEnemy), this, SymbolExtensions.GetMethodInfo(() => DisableEnemy()));
        Lua.RegisterFunction(nameof(EnableEnemy), this, SymbolExtensions.GetMethodInfo(() => EnableEnemy()));
    }

    public void DisableEnemy()
    {
        if (enemyObjects != null)
        {
            foreach (EnemyController enemy in control)
            {
                enemy.enabled = false;
            }
        }
        else
        {
            Debug.Log(enemyObjects);
        }
        if (npc != null)
        {
            foreach (Shoot nembak in shoot)
            {
                nembak.enabled = false;
            }
        }
        else
        {
            Debug.Log(npc);
        }
    }

    public void EnableEnemy()
    {
        if (enemyObjects != null)
        {
            foreach (EnemyController enemy in control)
            {
                enemy.enabled = true;
            }
        }
        else
        {
            Debug.Log(enemyObjects);
        }
        if (npc != null)
        {
            foreach (Shoot nembak in shoot)
            {
                nembak.enabled = true;
            }
        }
        else
        {
            Debug.Log(npc);
        }
    }
}
