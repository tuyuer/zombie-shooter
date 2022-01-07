﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    //input controls
    public List<InputControl> inputControls = new List<InputControl>();

    [Header("Double Tap Settings")]
    public float doubleTapSpeed = .3f;
    private float lastInputTime = 0f;
    private string lastInputAction = "";

    public delegate void DirectionInputHandler(Vector2 dir, Vector2 dirRaw, input_action_state inputState);
    public event DirectionInputHandler onDirectionEvent;

    public delegate void InputEventHandler(string action, input_action_state actionState);
    public event InputEventHandler onInputEvent;

    public delegate void MouseEventHandler(float mouseX, float mouseY);
    public event MouseEventHandler onMouseEvent;


    void Awake()
    {
        InitKeyboardInputs();
        OnInitExternalInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalDef.ENABLE_STICKJOY)
        {
            StickjoyControls();
        }
        else
        {
            KeyboardControls();
            MouseEvents();
        }
    }

    void KeyboardControls()
    {
        bool doubleTapState = false;
        foreach (InputControl inputControl in inputControls)
        {
            if (onInputEvent == null) continue;

            //on keyboard key down
            if (Input.GetKeyDown(inputControl.key))
            {
                doubleTapState = DetectDoubleTap(inputControl);
                onInputEvent(inputControl.action, input_action_state.press);
            }

            //on keyboard key up
            if (Input.GetKeyUp(inputControl.key))
            {
                onInputEvent(inputControl.action, input_action_state.release);
            }
        }

        //Store the input axes.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float hRaw = Input.GetAxisRaw("Horizontal");
        float vRaw = Input.GetAxisRaw("Vertical");


        input_action_state actionState = input_action_state.release;
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            actionState = input_action_state.hold;
        }
        else if (Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S))
        {
            actionState = input_action_state.press;
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEditor.EditorApplication.isPaused = true;
        }
#endif
        onDirectionEvent(new Vector2(h, v), new Vector2(hRaw, vRaw), actionState);
    }

    void StickjoyControls()
    {
        //Store the input axes.
        float h = GameWorld.Instance.joystick.Direction.x;
        float v = GameWorld.Instance.joystick.Direction.y;

        float hRaw = h > 0 ? 1 : 0;
        float vRaw = v > 0 ? 1 : 0;

        input_action_state actionState = input_action_state.release;
        if (GameWorld.Instance.joystick.Direction.sqrMagnitude > 0)
        {
            actionState = input_action_state.press;
        }
        onDirectionEvent(new Vector2(h, v), new Vector2(hRaw, vRaw), actionState);
    }

    void MouseEvents()
    {
        if (onMouseEvent != null)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            onMouseEvent(mouseX, mouseY);
        }
    }

    //returns true if a key double tap is detected
    bool DetectDoubleTap(InputControl inputControl)
    {
        bool doubleTapDetected = ((Time.time - lastInputTime < doubleTapSpeed) && (lastInputAction == inputControl.action));
        lastInputAction = inputControl.action;
        lastInputTime = Time.time;
        return doubleTapDetected;
    }


    void InitKeyboardInputs()
    {
        //attack
        inputControls.Add(new InputControl(InputActionNames.O, KeyCode.Mouse0));
        inputControls.Add(new InputControl(InputActionNames.X, KeyCode.Mouse1));
        inputControls.Add(new InputControl(InputActionNames.DODGE, KeyCode.E));
        inputControls.Add(new InputControl(InputActionNames.JUMP, KeyCode.Space));
        inputControls.Add(new InputControl(InputActionNames.SHOWSWEAPON, KeyCode.R));
    }

    virtual public void OnInitExternalInputs()
    {

    }
}
