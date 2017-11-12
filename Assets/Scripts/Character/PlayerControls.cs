using UnityEngine;
using System.Collections;
using System;

// PlayerController script requires thoses components and will be added if they aren't already there
[RequireComponent(typeof(PlayerMovement))]

public class PlayerControls : MonoBehaviour
{
    private PlayerMovement movementScript;
    private CheckPointActivator checkPointActivatorScript;
    private PlayerActions actionsScript;

    [SerializeField]
    private string usedJoystickName = "";

    private bool movementControlsEnable = false;

    public string UsedJoystickName
    {
        get { return usedJoystickName; }
        set { usedJoystickName = value; }
    }

    private void Awake()
    {
        movementScript = GetComponent<PlayerMovement>();
        checkPointActivatorScript = GetComponent<CheckPointActivator>();
        actionsScript = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        if (Time.deltaTime > 0)
        {
            Hashtable inputs = fetchInputs();

            if(movementControlsEnable)
            {
                UpdateMovement(inputs);
                UpdateCheckPointActivation(inputs);
            }

            UpdateActions(inputs);

            //TestInputs(inputs);
        }
    }

    // The Hashtable of inputs value must contain those keys:
    //-verticalInput
    //-horizontalInput
    //-xAxis
    //-yAxis
    //-submitInput
    //-startInput
    //-cancelInput
    private Hashtable fetchInputs()
    {
        Hashtable inputs = new Hashtable();

        //Get the inputs used for the movement
        inputs.Add("verticalInput", Input.GetAxisRaw(UsedJoystickName + " Vertical"));
        inputs.Add("horizontalInput", Input.GetAxisRaw(UsedJoystickName + " Horizontal"));
        inputs.Add("xAxis", Input.GetAxis(UsedJoystickName + " Right Stick X Axis"));
        inputs.Add("yAxis", Input.GetAxis(UsedJoystickName + " Right Stick Y Axis"));
        inputs.Add("submitInput", Input.GetButtonDown(UsedJoystickName + " Submit"));
        inputs.Add("startInput", Input.GetButtonDown(UsedJoystickName + " Start"));
        inputs.Add("cancelInput", Input.GetButtonDown(UsedJoystickName + " Cancel"));
        
        return inputs;
    }

    private void UpdateMovement(Hashtable inputs)
    {
        movementScript.UpdateMovement(inputs);
    }

    private void UpdateCheckPointActivation(Hashtable inputs)
    {
        if ((bool)inputs["submitInput"]) checkPointActivatorScript.ActivateCheckPoint();
    }

    private void UpdateActions(Hashtable inputs)
    {
        actionsScript.UpdateActions(inputs);
    }

    public void EnableMovementControls(bool enable)
    {
        movementControlsEnable = enable;
        movementScript.StartMovement(enable);
    }

    private void TestInputs(Hashtable inputs)
    {
        Debug.Log("verticalInput: " + (float)inputs["verticalInput"]);
        Debug.Log("horizontalInput: " + (float)inputs["horizontalInput"]);
        Debug.Log("xAxis: " + (float)inputs["xAxis"]);
        Debug.Log("yAxis: " + (float)inputs["yAxis"]);
        Debug.Log("submitInput: " + (bool)inputs["submitInput"]);
        Debug.Log("startInput: " + (bool)inputs["startInput"]);
        Debug.Log("cancelInput: " + (bool)inputs["cancelInput"]);
    }
}
