using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;

public class TestOmbak : MonoBehaviour
{
    private bool isKeluar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isKeluar)
        {
            Debug.Log("Keluar");
        }
        else
        {
            Debug.Log("Belum Keluar");
        }
    }

    private void OnDisable()
    {
        this.GetComponent<TestOmbak>().enabled = false;
    }
}
