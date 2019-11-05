using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPPacket.Packet;
using TPPacket.Class;

public class ConsoleViewLinkerObj : MonoBehaviour
{
    public Button BackToListButton;
    public Button EditAutoDriveButton;

    public Button SwitchDriveModeButton;
    public Button AddCurrentPositionButton;
    public InputField PositionNameField;

    public Button SwitchCamButton;
    public Button HideUIButton;

    public Button ResUpButton;
    public Button ResDownButton;

    private SocketLinkerObj SocketLinkerObj;
    private CarStatusUIObj CarStatusUIObj;

    void Start()
    {
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();
        CarStatusUIObj = gameObject.GetComponent<CarStatusUIObj>();

        BackToListButton.onClick.AddListener(BackToList);
        EditAutoDriveButton.onClick.AddListener(EditAutoDrive);

        SwitchDriveModeButton.onClick.AddListener(SwitchDriveMode);
        AddCurrentPositionButton.onClick.AddListener(AddCurrentPosition);

        SwitchCamButton.onClick.AddListener(SwitchCam);
        HideUIButton.onClick.AddListener(HideUI);

        ResUpButton.onClick.AddListener(ResolutionUp);
        ResDownButton.onClick.AddListener(ResolutionDown);
    }

    private void Update()
    {
        if (PositionNameField.text == "")
        {
            AddCurrentPositionButton.interactable = false;
        }
        else
        {
            AddCurrentPositionButton.interactable = true;
        }
    }

    private void BackToList()
    {
        SocketLinkerObj.clientSender.SendPacket(new ConsoleUpdatePacket(ConsoleMode.ViewBotList, 0));
    }

    private void EditAutoDrive()
    {
        ViewChanger.Instance.ChangeView(ViewChanger.ConsoleView.LocationList);
    }

    private void SwitchDriveMode()
    {
        SocketLinkerObj.clientSender.SendPacket(new DataUpdatePacket(gameObject.GetComponent<CarStatusUIObj>().CarDevice.modeType.Next()));
    }

    private void AddCurrentPosition()
    {
        SocketLinkerObj.clientSender.SendPacket(new CarGPSSpotStatusChangeReqPacket(GPSSpotManagerChangeType.AddSpot, 
            new GPSPosition(PositionNameField.text ,CarStatusUIObj.CarDevice.gPSPosition.latitude, CarStatusUIObj.CarDevice.gPSPosition.longitude)));

        PositionNameField.text = "";
    }

    private void SwitchCam()
    {
        SocketLinkerObj.clientSender.SendPacket(new CamConfigPacket(CamaraConfigType.ChangeCamara, true));
    }

    private void HideUI()
    {
        throw new System.NotImplementedException();
    }

    private void ResolutionUp()
    {
        SocketLinkerObj.clientSender.SendPacket(new CamResolutionReqPacket(CarStatusUIObj.CarDevice.camResolution - 1));
    }

    private void ResolutionDown()
    {
        SocketLinkerObj.clientSender.SendPacket(new CamResolutionReqPacket(CarStatusUIObj.CarDevice.camResolution + 1));
    }
}
