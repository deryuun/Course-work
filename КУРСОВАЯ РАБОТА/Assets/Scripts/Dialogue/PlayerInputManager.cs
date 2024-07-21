using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private OrdinaryPlayerController _controller;
    private DialogueManager _manager;
    private bool _submitPressed = false;
    private bool _interactPressed = false;

    public void Start()
    {
        _manager = DialogueManager.GetInstance();
    }
    public bool CanPlayerMove()
    {
        return _manager == null || !_manager.dialogueIsPlaying;
    }
    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactPressed = true;
        }
        else if (context.canceled)
        {
            _interactPressed = false;
        } 
    }
    public void Submit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _submitPressed = true;
        }
        else if (context.canceled)
        {
            _submitPressed = false;
        } 
    }

    public bool GetSubmitPressed() 
    {
        bool result = _submitPressed;
        _submitPressed = false;
        return result;
    }
    
    public bool GetInteractPressed() 
    {
        bool result = _interactPressed;
        _interactPressed = false;
        return result;
    }
    
    public void RegisterSubmitPressed() 
    {
        _submitPressed = false;
    }
}