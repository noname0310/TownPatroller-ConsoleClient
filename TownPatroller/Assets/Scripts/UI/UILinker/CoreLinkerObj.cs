using System.Collections.Generic;
using UnityEngine;
using TownPatroller.UI.ListManage;
using TPPacket.Packet;

public class CoreLinkerObj : MonoBehaviour
{
    List<ClientItemLinkerObj> clientItemLinkerObjs;
    List<PosItemLinkerObj> posItemLinkerObjs;

    public ClientsListController ClientsListController;
    public PositionListController PositionListController;

    public GameObject ClientsContentObj;
    public GameObject PositionsContentObj;

    public GameObject ClientItemPrefab;
    public GameObject PositionItemPrefab;

    public SocketLinkerObj SocketLinker;

    void Start()
    {
        clientItemLinkerObjs = new List<ClientItemLinkerObj>();
        posItemLinkerObjs = new List<PosItemLinkerObj>();

        ClientsListController = gameObject.AddComponent<ClientsListController>();
        ClientsListController.New(ClientsContentObj, ClientItemPrefab);

        PositionListController = gameObject.AddComponent<PositionListController>();
        PositionListController.New(PositionsContentObj, PositionItemPrefab);

        TPPacket.Class.GPSSpotManager gPSSpotManager = new TPPacket.Class.GPSSpotManager(0);
        gPSSpotManager.AddPos(new TPPacket.Class.GPSPosition("djskda", -100, 100));
        gPSSpotManager.AddPos(new TPPacket.Class.GPSPosition("sadsakd", 1221, 312));

        PositionListController.RanderList(gPSSpotManager);
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
    }
}