using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class CheckEnemiesCount : MonoBehaviour
{
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject curtain;
    void Update()
    {
        if (this.gameObject.GetComponentsInChildren<Transform>().Length == 1)
        {
            timeline.SetActive(true);
            if (curtain != null)
            {
                curtain.SetActive(true);
                Debug.Log("Curtain");
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Children = " + this.gameObject.GetComponentsInChildren<Transform>().Length);
        }
    }
}
