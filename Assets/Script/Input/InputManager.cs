using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private bool interactPressed = false;
    private bool attackPressed = false;
    private bool submitPressed = false;
    private Vector2 move = Vector2.zero;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance()
    {
        // Debug.Log("Getting Input Manager");
        return instance;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            move = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            move = context.ReadValue<Vector2>();
        }
    }

    public void OnInteractPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackPressed = true;
        }
        else if (context.canceled)
        {
            attackPressed = false;
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public Vector2 GetMoveValue()
    {
        return move;
    }

    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }
    public bool GetAttackPressed()
    {
        bool result = attackPressed;
        attackPressed = false;
        return result;
    }
    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }
    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }
}
