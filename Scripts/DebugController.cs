using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Props to matt from gamedev guide and then video for the framework and the idea https://www.youtube.com/watch?v=VzOEM-4A2OM
public class DebugController : MonoBehaviour
{
    [SerializeField] bool showConsole;
    bool showHelp;

    string input;

    public static DebugCommand ROSEBUD;
    public static DebugCommand<int> SET_BOMBS;
    public static DebugCommand<int> SET_BOOMERANGS;
    public static DebugCommand<int> SET_GOLD;
    public static DebugCommand HELP;
    

    public List<object> commandList;
    public void OnToggleDebug(InputValue value) //Important note, in order to use the On action methods you need a player input component on the same gameobject as the script
    {
        showConsole = !showConsole;
       // Debug.Log("debug key");
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
        
        ROSEBUD = new DebugCommand("rosebud", "Adds 1000 gold.", "rosebud", () =>
        {
            PlayerStats.Instance.currentMoney += 1000;
            PlayerStats.Instance.UIMan.coinGUIupdate();
        });

        

        SET_GOLD = new DebugCommand<int>("set_gold", "Sets the amount of gold", "set_gold <gold_amount>", (x) =>
        {
            PlayerStats.Instance.currentMoney = x;
            PlayerStats.Instance.UIMan.coinGUIupdate();
        });

        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = true;
            
        });
        SET_BOMBS = new DebugCommand<int>("set_bombs", "Sets the amount of regular bombs that the player has", "set_bombs <bombs_amount>", (x) =>
        {
            PlayerStats.Instance.regularBombs = x;
            PlayerStats.Instance.UIMan.BombCounterUpdate();
            
        });
        SET_BOOMERANGS = new DebugCommand<int>("set_boomerangs", "Sets the amount of boomerangs that the player has", "set_boomerangs <boomerangs_amount>", (x) =>
        {
            PlayerStats.Instance.Boomerangs = x;
            PlayerStats.Instance.UIMan.BoomerangCounterUpdate();

        });




        commandList = new List<object>
        {
            ROSEBUD,
            SET_BOMBS,
            SET_BOOMERANGS,
            SET_GOLD,
            HELP,
            
        };
    }
    Vector2 scroll;
    private void OnGUI()
    {
        if (!showConsole) { return; }

        float y = 0f; // we all float down here

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for(int i=0; i<commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');
        for(int i=0; i<commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    //Cast to this type and invoke the command.
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null) {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}
