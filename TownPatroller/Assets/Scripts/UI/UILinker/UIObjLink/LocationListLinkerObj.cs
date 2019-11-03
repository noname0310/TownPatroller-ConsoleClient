using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPPacket.Packet;
using TPPacket.Class;

public class LocationListLinkerObj : MonoBehaviour
{
    public Button BackToCamButton;
    public Button AddItemButton;

    public GameObject AddPosPanel;
    public InputField Name;
    public Button SetCurrentPositionButton;
    public InputField Latitude;
    public InputField Longitude;
    public Button AddPositionButton;
    public Button CloseButton;

    private SocketLinkerObj SocketLinkerObj;
    private CarStatusUIObj CarStatusUIObj;

    void Start()
    {
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();
        CarStatusUIObj = gameObject.GetComponent<CarStatusUIObj>();

        BackToCamButton.onClick.AddListener(BackToCam);
        AddItemButton.onClick.AddListener(OpenAddItemView);

        SetCurrentPositionButton.onClick.AddListener(SetCurrentPosition);
        AddPositionButton.onClick.AddListener(AddPosition);
        CloseButton.onClick.AddListener(Close);
    }

    private void Update()
    {
        if (Name.text == "" || Latitude.text == "" || Longitude.text == "")
        {
            AddPositionButton.interactable = false;
        }
        else
        {
            AddPositionButton.interactable = true;
        }
    }

    private void BackToCam()
    {
        ViewChanger.Instance.ChangeView(ViewChanger.ConsoleView.Console);
    }

    private void OpenAddItemView()
    {
        Name.text = "";
        Latitude.text = "";
        Longitude.text = "";

        AddPosPanel.SetActive(true);
    }

    private void SetCurrentPosition()
    {
        Latitude.text = CarStatusUIObj.CarDevice.gPSPosition.latitude.ToString();
        Longitude.text = CarStatusUIObj.CarDevice.gPSPosition.longitude.ToString();
    }

    private void AddPosition()
    {
        GPSPosition gPSPosition = new GPSPosition(Name.text, float.Parse(Latitude.text), float.Parse(Longitude.text));
        SocketLinkerObj.clientSender.SendPacket(new CarGPSSpotStatusChangeReqPacket(GPSSpotManagerChangeType.AddSpot, gPSPosition));
        AddPosPanel.SetActive(false);
    }

    private void Close()
    {
        AddPosPanel.SetActive(false);
    }
}
