using System.Collections;
using System.Collections.Generic;
using TPPacket.Packet;
using UnityEngine;

public class ViewChanger : MonoBehaviour
{
    public static ViewChanger Instance { get; set; }

    public GameObject Botlist;
    public GameObject Console;
    public GameObject LocationList;

    private SocketLinkerObj SocketLinkerObj;

    private void Start()
    {
        Instance = this; 
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();
    }

    public void ChangeView(ConsoleView consoleView)
    {
        switch (consoleView)
        {
            case ConsoleView.Botlist:
                Botlist.SetActive(true);
                Console.SetActive(false);
                LocationList.SetActive(false);
                SocketLinkerObj?.clientSender?.SendPacket(new ClientinfoReqPacket());
                break;
            case ConsoleView.Console:
                Botlist.SetActive(false);
                Console.SetActive(true);
                LocationList.SetActive(false);
                break;
            case ConsoleView.LocationList:
                Botlist.SetActive(false);
                Console.SetActive(false);
                LocationList.SetActive(true);
                break;
            default:
                break;
        }
    }

    public enum ConsoleView
    {
        Botlist,
        Console,
        LocationList
    }
}
