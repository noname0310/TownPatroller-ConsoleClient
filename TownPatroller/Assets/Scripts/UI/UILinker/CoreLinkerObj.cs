using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TownPatroller.UI.ListManage;
using TownPatroller.Console;
using TPPacket.Packet;

public class CoreLinkerObj : MonoBehaviour
{
    List<ClientItemLinkerObj> clientItemLinkerObjs;
    List<PosItemLinkerObj> posItemLinkerObjs;

    [HideInInspector]
    public ClientsListController ClientsListController;
    [HideInInspector]
    public PositionListController PositionListController;
    [HideInInspector]
    public ConsoleTextManager ConsoleTextManager;

    public GameObject ClientsContentObj;
    public GameObject PositionsContentObj;
    public GameObject ConsoleContentObj;
    public GameObject ConsoleScrollView;

    public GameObject ClientItemPrefab;
    public GameObject PositionItemPrefab;
    public Text ConsoleTextPrefab;

    public SocketLinkerObj SocketLinker;

    void Start()
    {
        clientItemLinkerObjs = new List<ClientItemLinkerObj>();
        posItemLinkerObjs = new List<PosItemLinkerObj>();

        ClientsListController = gameObject.AddComponent<ClientsListController>();
        ClientsListController.New(ClientsContentObj, ClientItemPrefab);

        PositionListController = gameObject.AddComponent<PositionListController>();
        PositionListController.New(PositionsContentObj, PositionItemPrefab);

        ConsoleTextManager = new ConsoleTextManager(ConsoleScrollView, ConsoleContentObj, ConsoleTextPrefab, this);
    }

    public void RegisterObj(ClientItemLinkerObj clientItemLinkerObj)
    {
        if (clientItemLinkerObjs.Contains(clientItemLinkerObj) == true)
            return;

        clientItemLinkerObjs.Add(clientItemLinkerObj);
        clientItemLinkerObj.SelectButton.onClick.AddListener(delegate () { OnClientSelectButtonEvent(clientItemLinkerObj); });
    }

    public void RegisterObj(PosItemLinkerObj posItemLinkerObj)
    {
        if (posItemLinkerObjs.Contains(posItemLinkerObj) == true)
            return;

        posItemLinkerObjs.Add(posItemLinkerObj);
        posItemLinkerObj.DeleteButton.onClick.AddListener(delegate () { OnPosDeleteButtonEvent(posItemLinkerObj); });
    }

    public void OnClientSelectButtonEvent(ClientItemLinkerObj clientItemLinkerObj)
    {
        SocketLinker.clientSender.SendPacket(new ConsoleUpdatePacket(ConsoleMode.ViewSingleBot, ulong.Parse(clientItemLinkerObj.ClientID.text)));
    }

    public void OnPosDeleteButtonEvent(PosItemLinkerObj posItemLinkerObj)
    {
        SocketLinker.clientSender.SendPacket(new CarGPSSpotStatusChangeReqPacket(GPSSpotManagerChangeType.RemoveSpot, posItemLinkerObj.Index));
    }
}