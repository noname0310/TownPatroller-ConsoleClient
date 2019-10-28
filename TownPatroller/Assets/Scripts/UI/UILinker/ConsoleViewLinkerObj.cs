using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPPacket.Packet;

public class ConsoleViewLinkerObj : MonoBehaviour
{
    public Button BackToList;
    public Button EditAutoDrive;

    public Button SwitchDriveMode;
    public Button AddCurrentPosition;

    public Button SwitchCam;
    public Button HideUI;

    private SocketLinkerObj SocketLinkerObj;

    void Start()
    {
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();

        BackToList.onClick.AddListener(BackToListE);
        EditAutoDrive.onClick.AddListener(EditAutoDriveE);

        SwitchDriveMode.onClick.AddListener(SwitchDriveModeE);
        AddCurrentPosition.onClick.AddListener(AddCurrentPositionE);

        SwitchCam.onClick.AddListener(SwitchCamE);
        HideUI.onClick.AddListener(HideUIE);
    }

    private void BackToListE()
    {
        SocketLinkerObj.clientSender.SendPacket(new ConsoleUpdatePacket(ConsoleMode.ViewBotList, 0));
    }

    private void EditAutoDriveE()
    {
        ViewChanger.Instance.ChangeView(ViewChanger.ConsoleView.LocationList);
    }

    private void SwitchDriveModeE()
    {
        SocketLinkerObj.clientSender.SendPacket(new DataUpdatePacket(gameObject.GetComponent<CarStatusUIObj>().CarDevice.modeType.Next()));
    }

    private void AddCurrentPositionE()
    {

    }

    private void SwitchCamE()
    {
        SocketLinkerObj.clientSender.SendPacket(new CamConfigPacket(CamaraConfigType.ChangeCamara, true));
    }

    private void HideUIE()
    {
        throw new System.NotImplementedException();
    }
}
