using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            this.gameObject.GetComponent<Gameplay>().enabled = false;
        }
        else if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            this.gameObject.GetComponent<Gameplay>().enabled = false;
        }
    }
}
