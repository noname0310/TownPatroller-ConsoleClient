using UnityEngine;
using TownPatroller.Console;

public class IGConsole : MonoBehaviour
{
    public static IGConsole Instance { get; set; }
    public InGameConsole Main;

    private void Awake()
    {
        Instance = this;
    }

    public void Init(InGameConsole MainConsole)
    {
        Main = MainConsole;
    }

    public void println(string msg)
    {
        if (ViewChanger.Instance.Console.activeSelf)
            Main.println(msg);
    }
}
