using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;

public class InputRegistration : MonoBehaviour
{
    protected static bool isRegistered = false;
    private bool didIRegister = false;
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        // Debug.Log("Awake");
    }

    void OnEnable()
    {
        if (!isRegistered)
        {
            isRegistered = true;
            didIRegister = true;
            controls.Enable();
            InputDeviceManager.RegisterInputAction("Moves", controls.Basic.Moves);
            InputDeviceManager.RegisterInputAction("Interact", controls.Basic.Interaction);
            InputDeviceManager.RegisterInputAction("Attack", controls.Basic.Attack);
            InputDeviceManager.RegisterInputAction("Submit", controls.Basic.Submit);
        }
    }

    private void OnDisable()
    {
        if (didIRegister)
        {
            isRegistered = false;
            didIRegister = false;
            controls.Disable();
            InputDeviceManager.UnregisterInputAction("Moves");
            InputDeviceManager.UnregisterInputAction("Interact");
            InputDeviceManager.UnregisterInputAction("Attack");
            InputDeviceManager.UnregisterInputAction("Submit");
        }
    }
}
