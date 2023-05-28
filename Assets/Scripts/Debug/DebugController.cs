using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    //references
    public PlayerBehaviour player;

    public static DebugCommand INVENORY_RESOURCES_CLEAR;

    public List<object> commandList;

    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();

        INVENORY_RESOURCES_CLEAR = new DebugCommand("inventory_resources_clear", "Completely clears reource inventory", "invetory_resources_clear", () =>
        {
            player.ClearResourceInventory();
        });

        commandList = new List<object>
        {
            INVENORY_RESOURCES_CLEAR,
        };
    }


    private void OnGUI()
    {
        if (!showConsole)
        {
            return;
        }

        float y = 0f;
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void HandleInput()
    {
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandID))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }

    
}
