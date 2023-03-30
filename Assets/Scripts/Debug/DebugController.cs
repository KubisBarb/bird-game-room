using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    //references
    PlayerBehaviour player;

    public static DebugCommand inventory_resources_clear;

    public List<object> commandList;

    /*public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }*/

    private void Awake()
    {
        player = GameObject.Find("player").GetComponent<PlayerBehaviour>();

        inventory_resources_clear = new DebugCommand("inventory_resources_clear", "Completely clears reource inventory", "invetory_resources_clear", () =>
        {
            player.ClearResourceInventory();
        });

        commandList = new List<object>
        {
            inventory_resources_clear,
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            showConsole = !showConsole;
        }
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

    }

    
}
