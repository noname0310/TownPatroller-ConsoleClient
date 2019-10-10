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

    public void Init(GameObject UILinker)
    {
        Main = UILinker.GetComponent<InGameConsole>();
    }

    public void println(string msg)
    {
        Main.println(msg);
    }
}
